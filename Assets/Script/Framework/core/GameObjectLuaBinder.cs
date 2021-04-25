using System;
using System.Runtime.CompilerServices;
using Babeltime.Log;
using Framework.UI;
using LuaInterface;
using UnityEngine;

namespace Framework.core
{
    public class GameObjectLuaBinder:MonoBehaviour
    {
        private const string DESTROY = "Destroy";
        private const string GAMEOBJECT_LUABINDER = "___GameObjectLuaBinder___";
        private LuaTable luaObj;

        protected bool hasLuaObj()
        {
            return luaObj != null;
        }
        public void PushLuaTable()
        {
            if (luaObj != null)
            {
                var ls = luaObj.GetLuaState();
                ls.LuaGetRef(luaObj.GetReference());
            }
            else
            {
                throw new Exception("luaObj is null");
            }
            
        }
        
        protected void BindLuaTable(LuaTable value)
        {
            luaObj = value;
            BTLog.Error("SetLuaTable2:{0}", luaObj == null);
            
            var ls = value.GetLuaState();
            var luaRef = value.GetReference();
            if (value != null)
            {
                ls.LuaGetRef(luaRef);
                ls.LuaPushFunction(DestroyFromLua);
                ls.LuaSetField(-2, "DestroyToCS");

                ls.LuaPushValue(-1);
                ls.PushVariant(this);
                ls.LuaSetTable(LuaIndexes.LUA_REGISTRYINDEX);
                
                
                ls.LuaPushValue(-1);
                var obj = ls.ToVariant(-1);
                BTLog.Error("obj==================:{0}", obj);
                ls.LuaGetTable(LuaIndexes.LUA_REGISTRYINDEX);
                var obj2 = ls.ToVariant(-1);
                BTLog.Error("obj2=================:{0}", obj2);
                ls.LuaPop(1);
                
                var curTop = ls.LuaGetTop();
                ls.LuaGetField(-1, "DispatchMessage");
                if (ls.LuaIsNil(-1))
                {
                    ls.LuaSetTop(curTop);
                    throw new LuaException("can not find function DispatchMessage");
                }
                ls.LuaInsert(-2);
                ls.LuaPushString("OnBind");
                ls.LuaSafeCall(2, 0, 0, curTop);
            }
            else
            {

            }
            
        }

//        public static int vvv(IntPtr L)
//        {
//            return 0;
//        }

        protected void OnDestroy()
        {
            BTLog.Error("OnDestroy:{0}", name);
            if (luaObj == null)
            {
                return;
            }
            BTLog.Error("Do OnDestroy:{0}", name);
            var ls = luaObj.GetLuaState();
            var curTop = ls.LuaGetTop();
            var luaObjRef = luaObj.GetReference();
            ls.LuaGetRef(luaObjRef);
            ls.LuaGetRef(luaObjRef); 
            ls.LuaGetField(-1, DESTROY);
            if (ls.LuaIsNil(-1))
            {
                ls.LuaSetTop(curTop);
                return;
            }

            if (ls.lua_isboolean(-1))
            {
                BTLog.Error("is bool");
            }
            ls.LuaInsert(-2);
            ls.LuaSafeCall(1, 0, 0, 0);
        }

        private void CSDestroy()
        {
            BTLog.Error("CSDestroy:{0}", name);
            luaObj.Dispose();
            luaObj = null;
            GameObject.Destroy(gameObject);
        }
        private static int DestroyFromLua(IntPtr L)
        {
            try
            {
//                BTLog.Error("cs call vvv");
                ToLua.CheckArgsCount(L, 1);
//                BTLog.Error("cs call vvv1 top:{0}", LuaDLL.lua_gettop(L));
                LuaDLL.lua_pushnil(L);
                LuaDLL.lua_setfield(L, -2, "DestroyToCS");
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                var binder = ToLua.ToVarObject(L, -1) as GameObjectLuaBinder;
                binder.CSDestroy();
                return 0;
            }
            catch (Exception e)
            {
                return LuaDLL.toluaL_exception(L, e);
            }
        }

        protected LuaState GetLuaState()
        {
            return luaObj.GetLuaState();
        }

        public virtual void CreatePrefabAndBindLuaClass(LuaState luaState)
        {
            BTLog.Error("CreatePrefabAndBindLuaClass");
        }
    }
}