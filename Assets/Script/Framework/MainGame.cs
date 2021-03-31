using System;
using LuaInterface;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using Babeltime.Log;
using Framework.core;

public class MainGame : MonoBehaviour
{
    private LuaState luaState;

    public LuaState LuaState
    {
        get { return luaState; }
    }

    private static MainGame ins;

    public static MainGame Ins
    {
        get { return ins; }
    }

    private GameObject uiRoot;
//    private GameObject uiStage;
//    private GameObject uiBackstage;
    
    private LuaFunction luaBridge;

    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
        }
        CreateUIStage();
        luaState = new LuaState();
        OpenLibs();
        luaState.LuaSetTop(0);
        Bind();        
        LoadLuaFiles();
#if UNITY_EDITOR && !LOADFROM_BUNDLE
        BTLog.Debug("open HotFixLua");
        var hot = this.gameObject.AddComponent<HotFixLua>();
        hot.LuaPath = "/Lua/";
#endif
    }

    protected virtual void OpenLibs()
    {
        luaState.OpenLibs(LuaDLL.luaopen_pb);
        luaState.OpenLibs(LuaDLL.luaopen_struct);
        luaState.OpenLibs(LuaDLL.luaopen_lpeg);
        OpenCJson();
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        luaState.OpenLibs(LuaDLL.luaopen_bit);
#endif

        if (LuaConst.openLuaSocket)
        {
            OpenLuaSocket();            
        }        

        if (LuaConst.openLuaDebugger)
        {
            OpenZbsDebugger();
        }
        
    }
    
    protected virtual void Bind()
    {        
        LuaBinder.Bind(luaState);
        DelegateFactory.Init();   
        LuaCoroutine.Register(luaState, this);        
    }
    
    protected virtual void LoadLuaFiles()
    {
        luaState.Start();
        StartMain();
    }
    
    protected virtual void StartMain()
    {
        luaState.AddSearchPath(LuaConst.luaDir);
        luaState.AddSearchPath(LuaConst.toluaDir);
        luaState.DoFile("Framework\\Main.lua");
        CallMain();
        luaBridge = luaState.GetFunction("LuaBridge");
    }
    
    protected virtual void CallMain()
    {
        LuaFunction main = luaState.GetFunction("Main");
#if UNITY_EDITOR
        main.Call(true);
#else
        main.Call(false);
#endif
        main.Dispose();
    }
    
    protected void OpenLuaSocket()
    {
        LuaConst.openLuaSocket = true;

        luaState.BeginPreLoad();
        luaState.RegFunction("socket.core", LuaOpen_Socket_Core);
        luaState.RegFunction("mime.core", LuaOpen_Mime_Core);                
        luaState.EndPreLoad();                     
    }
    
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Socket_Core(IntPtr L)
    {        
        return LuaDLL.luaopen_socket_core(L);
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Mime_Core(IntPtr L)
    {
        return LuaDLL.luaopen_mime_core(L);
    }
    
    public void OpenZbsDebugger(string ip = "localhost")
    {
        if (!Directory.Exists(LuaConst.zbsDir))
        {
            Debugger.LogWarning("ZeroBraneStudio not install or LuaConst.zbsDir not right");
            return;
        }

        if (!LuaConst.openLuaSocket)
        {                            
            OpenLuaSocket();
        }

        if (!string.IsNullOrEmpty(LuaConst.zbsDir))
        {
            luaState.AddSearchPath(LuaConst.zbsDir);
        }

        luaState.LuaDoString(string.Format("DebugServerIp = '{0}'", ip), "@LuaClient.cs");
    }
    
    protected void OpenCJson()
    {
        luaState.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
        luaState.OpenLibs(LuaDLL.luaopen_cjson);
        luaState.LuaSetField(-2, "cjson");

        luaState.OpenLibs(LuaDLL.luaopen_cjson_safe);
        luaState.LuaSetField(-2, "cjson.safe");                               
    }
    
    // Update is called once per frame
    void Update()
    {
        CSBridge.LoadAsset();
        luaBridge.Call("Update", Time.time, Time.unscaledTime);
    }

    private void FixedUpdate()
    {
        luaBridge.Call("FixedUpdate", Time.fixedTime, Time.fixedUnscaledTime);
    }

    public void GetPrefabLua(int contextId)
    {
        luaState.LuaGetGlobal("prefabIDMap");
        if (luaState.LuaIsNil(-1))
        {
            luaState.LuaPop(1);
            BTLog.Error("can nof find global table prefabIDMap");
            return;
        }
        luaState.Push(contextId);
        luaState.LuaGetTable(-2);
        if (luaState.LuaIsNil(-1))
        {
            luaState.LuaPop(2);
            BTLog.Error(string.Format("load prefab document but can not find contextID:{0}", contextId));
            return;
        }
        luaState.LuaReplace(-2);
    }

    private void CreateUIStage()
    {
        uiRoot = GameObject.Find("UIRoot");
        if (uiRoot == null)
        {
            throw new Exception("can not find UIRoot");
        }
//        uiStage = GameObject.Find("UIStage");
//        if (uiStage == null)
//        {
//            throw new Exception("can not find uiStage");
//        }
//        uiBackstage = GameObject.Find("UIBackstage");
//        if (uiBackstage == null)
//        {
//            throw new Exception("can not find uiBackstage");
//        }
    }

//    TODO 暂时不考虑使用backstage，因为active = false目前已经足够了
//    public void AddChildToBackstage(GameObject child)
//    {
//        child.transform.parent = uiBackstage.transform;
//    }

    public void AddChild2Stage(GameObject child)
    {
//        child.transform.parent = uiStage.transform;
        child.transform.parent = uiRoot.transform;
    }
    
    
}
