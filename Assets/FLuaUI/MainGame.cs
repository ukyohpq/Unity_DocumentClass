using System;
using LuaInterface;
using UnityEngine;
using System.IO;
using FLua.Log;
using FLuaUI.core;
using FLuaUI.core.loader;
using FLuaUI.LuaUI.Components;
using FLuaUI.LuaUI.UIExtends;

namespace FLuaUI
{
    public class MainGame : MonoBehaviour
    {
        public string luaLogicPath;
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

        private GameObject normalUIStage;

        private GameObject draggingUIStage;
//    private GameObject uiStage;
//    private GameObject uiBackstage;
        private void Awake()
        {
            if (ins == null)
            {
                ins = this;
            }

            CreateUIStage();
            InitLua();
        }

        private void InitLua()
        {
            luaState = new LuaState();
            OpenLibs();
            luaState.LuaSetTop(0);
            Bind();
            BtLuaScripts.Init(luaState);
            LoadLuaFiles();
#if UNITY_EDITOR
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
//            var mainPath = "Framework\\Main.lua";
//            luaState.LuaLoadBuffer(BtLuaScripts.ReqireLua(mainPath), mainPath.Length,mainPath);
            luaState.DoFile("Framework\\Main.lua");
            RequireBaseFiles();
            LuaUIExtendManager.InitExtends(luaState);
            CallMain();
        }

        private void RequireBaseFiles()
        {
            luaState.LuaGetGlobal("requireLuaUIFramework");
            if (luaState.LuaIsNil(-1))
            {
                BTLog.Error("can not find lua requireLuaUIFramework function");
                luaState.LuaPop(1);
                return;
            }
#if UNITY_EDITOR
            luaState.LuaPushBoolean(true);
#else
        luaState.LuaPushBoolean(false);
#endif
            luaState.LuaSafeCall(1, 0, 0, 0);
        }

        protected virtual void CallMain()
        {
            luaState.LuaGetGlobal("Main");
            if (luaState.LuaIsNil(-1))
            {
                BTLog.Error("can not find lua Main function");
                luaState.LuaPop(1);
                return;
            }
#if UNITY_EDITOR
            luaState.LuaPushBoolean(true);
#else
        luaState.LuaPushBoolean(false);
#endif
            luaState.LuaPushString(luaLogicPath);
            luaState.LuaSafeCall(2, 0, 0, 0);
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
            LoaderManager.LoadAsset();
            UpHandler.GetInstance().Update();
            luaState.LuaGetGlobal("LuaBridge");
            if (luaState.LuaIsNil(-1))
            {
                BTLog.Debug("can not find lua function: LuaBridge");
                luaState.LuaPop(1);
                return;
            }

            luaState.LuaPushString("Update");
            luaState.LuaPushNumber(Time.time);
            luaState.LuaPushNumber(Time.unscaledTime);
            luaState.LuaSafeCall(3, 0, 0, 0);
        }

        private void FixedUpdate()
        {
            luaState.LuaGetGlobal("LuaBridge");
            if (luaState.LuaIsNil(-1))
            {
                BTLog.Debug("can not find lua function: LuaBridge");
                luaState.LuaPop(1);
                return;
            }

            luaState.LuaPushString("FixedUpdate");
            luaState.LuaPushNumber(Time.fixedTime);
            luaState.LuaPushNumber(Time.fixedUnscaledTime);
            luaState.LuaSafeCall(3, 0, 0, 0);
        }

        private void CreateUIStage()
        {
            uiRoot = GameObject.Find("UIRoot");
            if (uiRoot == null)
            {
                throw new Exception("can not find UIRoot");
            }
            
            normalUIStage = GameObject.Find("UIRoot/NormalUIStage");
            if (normalUIStage == null)
            {
                normalUIStage = new GameObject("NormalUIStage");
                normalUIStage.transform.parent = uiRoot.transform;
                normalUIStage.transform.localPosition = Vector3.zero;
            }
            
            draggingUIStage = GameObject.Find("UIRoot/DraggingUIStage");
            if (draggingUIStage == null)
            {
                draggingUIStage = new GameObject("DraggingUIStage");
                draggingUIStage.transform.parent = uiRoot.transform;
                draggingUIStage.transform.localPosition = Vector3.zero;
                var cg = draggingUIStage.AddComponent<CanvasGroup>();
                cg.interactable = false;
                cg.blocksRaycasts = false;
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
            child.transform.parent = normalUIStage.transform;
        }

        public void MoveToDraggingStage(GameObject obj)
        {
            obj.transform.parent = draggingUIStage.transform;
        }
    }
}