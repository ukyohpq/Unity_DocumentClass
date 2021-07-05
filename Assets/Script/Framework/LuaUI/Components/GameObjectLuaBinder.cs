using System;
using Babeltime.Log;
using LuaInterface;
using UnityEngine;

namespace Framework.core.Components
{
    public class GameObjectLuaBinder:MonoBehaviour
    {
        public Transform Container;
        private const string DESTROY = "Destroy";
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
        
        public void BindLuaTable(LuaTable value)
        {
            luaObj = value;
            var ls = value.GetLuaState();
            var luaRef = value.GetReference();
            if (value != null)
            {
                ls.LuaGetRef(luaRef);
//                luaObj.DestroyToCS = this.DestroyToCS
//                为了实现仅能调用一次销毁，函数DestroyToCS必须放在instance上，而不能放在class上
                ls.LuaPushFunction(DestroyFromLua);
                ls.LuaSetField(-2, "DestroyToCS");
//                luaObj.name = this.name;
                ls.LuaPushString(name);
                ls.LuaSetField(-2, "name");

//                registry[luaObj] = this;
                ls.LuaPushValue(-1);
                ls.PushVariant(this);
                ls.LuaSetTable(LuaIndexes.LUA_REGISTRYINDEX);
                
//                luaObj:DispatchMessage("OnBind")
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

        protected void OnDestroy()
        {
            if (luaObj == null)
            {
                return;
            }
//            luaObj:Destroy()
            var ls = luaObj.GetLuaState();
            var curTop = ls.LuaGetTop();
            var luaObjRef = luaObj.GetReference();
            ls.LuaGetRef(luaObjRef);
            ls.LuaPushValue(-1); 
            ls.LuaGetField(-1, DESTROY);
            if (ls.LuaIsNil(-1))
            {
                ls.LuaSetTop(curTop);
                return;
            }
            
            ls.LuaInsert(-2);
            ls.LuaSafeCall(1, 0, 0, 0);
        }
        
        private static int DestroyFromLua(IntPtr L)
        {
            try
            {
//                BTLog.Error("cs call vvv");
                ToLua.CheckArgsCount(L, 1);
//                BTLog.Error("cs call vvv1 top:{0}", LuaDLL.lua_gettop(L));
                LuaDLL.lua_pushvalue(L, -1);
                LuaDLL.lua_pushnil(L);
                LuaDLL.lua_setfield(L, -2, "DestroyToCS");
                LuaDLL.lua_gettable(L, LuaIndexes.LUA_REGISTRYINDEX);
                var binder = ToLua.ToVarObject(L, -1) as GameObjectLuaBinder;
                binder.luaObj.Dispose();
                binder.luaObj = null;
                LuaDLL.lua_pushnil(L);
                LuaDLL.lua_settable(L, LuaIndexes.LUA_REGISTRYINDEX);
//                BTLog.Error("DestroyFromLua:{0}", binder.name);
                GameObject.Destroy(binder.gameObject);
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

        protected void PushLuaInstance(LuaState luaState, string className)
        {
            var curTop = luaState.LuaGetTop();
            luaState.LuaGetGlobal(className);
            if (luaState.LuaIsNil(-1))
            {
                luaState.LuaSetTop(curTop);
                BTLog.Error("can not find lua class:{0}", className);
                return;
            }
            
//            cache old bind
//            class oldbind
            luaState.LuaGetField(-1, "bind");
//            从cs中创建的prefab对象不需要LoadResource，故这里将LoadResource重置
            luaState.LuaPushFunction(EmptyLuaFunc);
            luaState.LuaSetField(-3, "bind");
            luaState.LuaGetField(-2, "New");
            if (luaState.LuaIsNil(-1))
            {
                luaState.LuaSetTop(curTop);
                BTLog.Error("can not find constructor for lua class:{0}", className);
                return;
            }
            
            luaState.LuaSafeCall(0, 1, 0, curTop);

//            将实例和类换一下位置，这里需要将class上的LoadResource抹掉
            luaState.LuaInsert(-3);
            luaState.LuaSetField(-2, "bind");
//          删除luaclass
            luaState.LuaRemove(-1);
            var tb = luaState.ToVariant(-1) as LuaTable;
            BindLuaTable(tb);
        }
        public virtual void CreatePrefabAndBindLuaClass(LuaState luaState)
        {
            BTLog.Debug("CreatePrefabAndBindLuaClass");
        }

        private static int EmptyLuaFunc(IntPtr L)
        {
            return 0;
        }

        public virtual string GetLuaClassName()
        {
            throw new Exception("must be override");
        }
        
    }
}