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
            if (luaObj == value)
            {
                return;
            }
            luaObj = value;
            BTLog.Error("SetLuaTable2:{0}", luaObj == null);
            
            var ls = value.GetLuaState();
            var luaRef = value.GetReference();
            if (value != null)
            {
                ls.LuaGetRef(luaRef);
                ls.LuaPushFunction(vvv);
                ls.LuaSetField(-2, "vvv");
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
            ls.LuaInsert(-2);
            ls.LuaSafeCall(1, 0, 0, 0);
        }

        private int vvv(IntPtr L)
        {
            try
            {
                BTLog.Error("cs call vvv");
                ToLua.CheckArgsCount(L, 1);
                BTLog.Error("this:{0}", this == null);
                BTLog.Error("top:{0}", LuaDLL.lua_gettop(L));
                LuaDLL.lua_getfield(L, -1, GAMEOBJECT_LUABINDER);
                var ob = ToLua.ToVarObject(L, -1) as DocumentClass;
                BTLog.Error("ob == null:{0} {1} {2}", ob == null, ob.ToString(), LuaDLL.lua_gettop(L));
                LuaDLL.lua_settop(L, 0);
                BTLog.Error("destroy gameobject:{0}", ob.name);
                var luaObj = ob.luaObj;
                luaObj.Dispose();
                ob.luaObj = null;
                GameObject.Destroy(ob.gameObject);
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