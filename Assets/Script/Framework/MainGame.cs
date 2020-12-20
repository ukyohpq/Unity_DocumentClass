using System;
using LuaInterface;
using UnityEngine;
using System.IO;
using Babeltime.Log;

public class MainGame : MonoBehaviour
{
    private LuaState luaState;
    private LuaFunction luaUpdate;
    private LuaFunction luaFixedUpdate;
    private void Awake()
    {
        luaState = new LuaState();
        OpenLibs();
        luaState.LuaSetTop(0);
        Bind();        
        LoadLuaFiles();
#if UNITY_EDITOR && !LOADFROM_BUNDLE
        var hot = this.gameObject.AddComponent<HotFixLua>();
        hot.LuaPath = "\\Lua";
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
        luaUpdate = luaState.GetFunction("Update");
        luaFixedUpdate = luaState.GetFunction("FixedUpdate");
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
        luaUpdate.Call(Time.time, Time.unscaledTime);
    }

    private void FixedUpdate()
    {
        luaFixedUpdate.Call(Time.fixedTime, Time.fixedUnscaledTime);
    }
}
