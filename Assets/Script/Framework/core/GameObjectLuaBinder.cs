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
        protected void PushLuaTable()
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
//                ls.LuaPushFunction(vvv);
//                ls.LuaSetField(-2, "vvv");
                ls.PushVariant(this);
                ls.LuaSetField(-2, GAMEOBJECT_LUABINDER);
                ls.LuaPop(1);
            }
            else
            {

            }
            
        }


        protected void OnDestroy()
        {
            BTLog.Error("OnDestroy:{0}", name);
            if (luaObj == null)
            {
                return;
            }
            BTLog.Error("Do OnDestroy:{0}", name);
            var ls = luaObj.GetLuaState();
            var luaObjRef = luaObj.GetReference();
            ls.LuaGetRef(luaObjRef);
            ls.LuaGetRef(luaObjRef); 
            ls.LuaGetField(-1, DESTROY);
            if (ls.LuaIsNil(-1))
            {
                ls.LuaSetTable(0);
                return;
            }

            if (ls.lua_isboolean(-1))
            {
                BTLog.Error("is bool");
            }
            ls.LuaInsert(-2);
            ls.LuaSafeCall(1, 0, 0, 0);
        }

        public void CSDestroy()
        {
            BTLog.Error("CSDestroy:{0}", name);
            var ls = luaObj.GetLuaState();
            ls.LuaGetRef(luaObj.GetReference());
            ls.LuaPushNil();
            ls.LuaSetField(-2, GAMEOBJECT_LUABINDER);
            luaObj.Dispose();
            luaObj = null;
            GameObject.Destroy(gameObject);
        }
        private int vvv(IntPtr L)
        {
            try
            {
                BTLog.Error("cs call vvv");
                ToLua.CheckArgsCount(L, 1);
                BTLog.Error("cs call vvv1");
//                LuaDLL.lua_getfield(L, -1, GAMEOBJECT_LUABINDER);
//                var obj = ToLua.ToVarObject(L, -1);
//                BTLog.Error("cs call vvv2:{0}", obj);
//                var ob = ToLua.ToVarObject(L, -1) as DocumentClass;
//                BTLog.Error("cs call vvv3");
//                LuaDLL.lua_pop(L, 1);
//                BTLog.Error("cs call vvv4");
//                LuaDLL.lua_pushnil(L);
//                BTLog.Error("cs call vvv5");
//                LuaDLL.lua_setfield(L, -1, GAMEOBJECT_LUABINDER);
//                BTLog.Error("cs call vvv6");
//                LuaDLL.lua_pop(L, 1);
//                BTLog.Error("cs call vvv7");
//                var luaObj = ob.luaObj;
//                BTLog.Error("cs call vvv8");
//                luaObj.Dispose();
//                BTLog.Error("cs call vvv9");
//                ob.luaObj = null;
//                BTLog.Error("cs call vvv10");
//                GameObject.Destroy(ob.gameObject);
//                BTLog.Error("cs call vvv11");
//                LuaDLL.lua_settop(L, 0);
//                BTLog.Error("cs call vvv12");
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
    }
}