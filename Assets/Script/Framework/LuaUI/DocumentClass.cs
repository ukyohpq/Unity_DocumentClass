using System;
using Babeltime.Log;
using LuaInterface;
using UnityEngine;
using Framework.core;

namespace Framework.LuaUI
{
    public class DocumentClass : GameObjectLuaBinder
    {
        public static void Extend(LuaState ls)
        {
            ls.LuaGetGlobal("Prefab");
            if (ls.LuaIsNil(-1))
            {
                ls.LuaPop(1);
                throw new Exception("can not find lua Class: Prefab");
            }
            ls.LuaPushFunction(PrefabBind);
            ls.LuaSetField(-2, "bind");
            ls.LuaPop(1);
        }

        private static int PrefabBind(IntPtr L)
        {
            try
            {
                ToLua.CheckArgsCount(L, 1);
//                dup self
                LuaDLL.lua_pushvalue(L, -1);
                LuaDLL.lua_getglobal(L, "Prefab");
                LuaDLL.lua_getfield(L, -1, "super");
//                Prefab.super:bind(self)
                LuaDLL.lua_getfield(L, -1, "bind");
//                self, self, Prefab, super, bind
                LuaDLL.lua_insert(L, -4);
//                self, bind, self, Prefab, super
                LuaDLL.lua_pop(L, 2);
//                self, bind, self,
                LuaDLL.lua_call(L, 1, 0);
                
                var tb = ToLua.ToLuaTable(L, -1);
                CSBridge.LoadPrefab(tb);
                
                LuaDLL.lua_getfield(L, -1, "afterBind");
                LuaDLL.lua_setfield(L, -2, "bind");
                
                LuaDLL.lua_pushcfunction(L, DestroyFromLua);
                LuaDLL.lua_setfield(L, -2, "DestroyToCS");
                return 0;
            }
            catch (Exception e)
            {
                return LuaDLL.toluaL_exception(L, e);
            }
        }

        private static int DestroyFromLua(IntPtr L)
        {
            try
            {
                ToLua.CheckArgsCount(L, 1);
                LuaDLL.lua_pushnil(L);
                LuaDLL.lua_setfield(L, -2, "DestroyToCS");
                var tb = ToLua.ToLuaTable(L, -1);
                CSBridge.StopLoad(tb);
                return 0;
            }
            catch (Exception e)
            {
                return LuaDLL.toluaL_exception(L, e);
            }
        }
        
        
        [SerializeField]
        private string LuaClass = "";
        
        [NoToLua]
        public void SetLuaTable(LuaTable value)
        {
            BindLuaTable(value);
            var luaState = value.GetLuaState();
#if UNITY_EDITOR
            var oldTop = luaState.LuaGetTop();
#endif
            luaState.LuaGetRef(value.GetReference());
            BindLuaClass(luaState);
#if UNITY_EDITOR
            var curTop = luaState.LuaGetTop();
            if (curTop != oldTop)
            {
                BTLog.Error("stack is unbalanced oldTop:{0} curTop:{1}", oldTop, curTop);
            }
#endif
        }

        public string GetLuaClassName()
        {
            return LuaClass;
        }
        
//        该函数在调用前，必须保证当前lua栈顶有一个lua的Prefab对象。也就是说，栈不为空!
        private void BindLuaClass(LuaState luaState)
        {
            if (luaState.LuaIsNil(-1))
            {
                BTLog.Error("该函数在调用前必须保证lua栈顶上有一个lua的Prefab对象");
                return;
            }
            BindFieldsOnTrans(transform, luaState, luaState.LuaGetTop());
//            完成绑定之后，广播complete事件
            luaState.LuaGetField(-1, "DispatchMessage");
            if (luaState.LuaIsNil(-1))
            {
                luaState.LuaPop(1);
                BTLog.Warning("Prefab Lua must has Method:DispatchMessage");
                return;
            }
            luaState.LuaInsert(-2);
            luaState.Push("COMPLETE");
            luaState.LuaSafeCall(2, 0, 0, 0);
        }

        private void BindFieldsOnTrans(Transform trans, LuaState luaState, int topIdx)
        {
            BTLog.Debug("BindFieldsOnTrans trans:{0} top:{1}", trans.name, topIdx);
            var numChildren = trans.childCount;
            for (int i = 0; i < numChildren; i++)
            {
                var child = trans.GetChild(i);
                var childName = child.name;
                if (childName == "") continue;
                var suffix = Utils.GetSuffixOfGoName(childName);
//                如果不是合法后缀，则直接进入下一级，检测子go有没有需要绑定的
                if (!Utils.IsValidSuffix(suffix))
                {
                    BindFieldsOnTrans(child, luaState, topIdx);
                    continue;
                }
//                对Doc进行特殊处理，这个不能直接绑定cs组件，需要创建一个lua对象，然后进行绑定
                switch (suffix)
                {
                    case "_Doc":
                        var childDoc = child.GetComponent<DocumentClass>();
                        childDoc.CreatePrefabAndBindLuaClass(luaState);
//                        parent child
                        childDoc.PushLuaTable();
//                        parent child parent
                        luaState.LuaPushValue(-2);
//                        parent child parent AddChild
                        luaState.LuaGetField(-1, "AddChild");
//                        parent AddChild child parent
                        luaState.LuaInsert(-3);
//                        parent AddChild parent child
                        luaState.LuaInsert(-2);
//                        parent child
                        luaState.LuaSafeCall(2, 1, 0, 0);
                        luaState.LuaSetField(topIdx, childName);
                        break;
                    case "_Button":
                        var childBtn = child.GetComponent<LuaButton>();
                        if (childBtn == null)
                        {
                            childBtn = child.gameObject.AddComponent<LuaButton>();
                        }

                        childBtn.CreatePrefabAndBindLuaClass(luaState);
                        luaState.LuaPushValue(-2);
                        luaState.LuaGetField(-1, "AddChild");
                        luaState.LuaInsert(-3);
                        luaState.LuaInsert(-2);
                        luaState.LuaSafeCall(2, 1, 0, 0);
                        luaState.LuaSetField(topIdx, childName);
                        break;
                    case "_Image":
                        var childImage = child.GetComponent<LuaImage>();
                        if (childImage == null)
                        {
                            childImage = child.gameObject.AddComponent<LuaImage>();
                        }

                        childImage.CreatePrefabAndBindLuaClass(luaState);
                        luaState.LuaPushValue(-2);
                        luaState.LuaGetField(-1, "AddChild");
                        luaState.LuaInsert(-3);
                        luaState.LuaInsert(-2);
                        luaState.LuaSafeCall(2, 1, 0, 0);
                        luaState.LuaSetField(topIdx, childName);
                        break;
                    default:
                        var T = Utils.GetTypeByComponentSuffix(suffix);
                        if (T == null) continue;
                        luaState.PushVariant(child.GetComponent(T));
                        luaState.LuaSetField(topIdx, childName);
                        break;
                }

                BTLog.Debug("bind {0}. name:{1} childName:{2}", suffix, trans.name, childName);

            }
        }
        
//        TODO 目前暂时确定父类一定是Framework.UI.Prefab类，不使用自定义父类，因为检测自定义父类是从Framework.UI.Prefab继承而来，比较麻烦，而且考虑使用状态而不是继承来重用Prefab
//        [SerializeField]
//        private string SuperClass = "";
        // Start is called before the first frame update
        void Start()
        {
            if (!hasLuaObj())
            {
                CreatePrefabAndBindLuaClass(MainGame.Ins.LuaState);
            }
//            BTLog.Error("documentclass contextID:{0}", contextID);
        }

//        通过在cs端创建lua的Prefab对象进行绑定
        public override void CreatePrefabAndBindLuaClass(LuaState luaState)
        {
            base.CreatePrefabAndBindLuaClass(luaState);
            PushLuaInstance(luaState, Utils.MakeClassName(LuaClass));
            BindLuaClass(luaState);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        
        protected void OnDestroy()
        {
            base.OnDestroy();
        }
        
        
    }
}

