using Babeltime.Log;
using LuaInterface;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BtLuaScripts
{
    private static AssetBundle LuaScriptBundle;
    private static AssetBundle ToLuaScriptBundle;
    public static void Init(LuaState ls)
    {
#if !UNITY_EDITOR || USE_BUNDLE
        if (LuaScriptBundle != null)
        {
            LuaScriptBundle.Unload(true);
            LuaScriptBundle = null;
        }
        if (ToLuaScriptBundle != null)
        {
            ToLuaScriptBundle.Unload(true);
            ToLuaScriptBundle = null;
        }
        ToLuaScriptBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/toluabundle");
        LuaScriptBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/luabundle");
        AddLoader(ls);
        foreach (var VARIABLE in ToLuaScriptBundle.GetAllAssetNames())
        {
            BTLog.Error("tolua:{0}", VARIABLE);
        }

        foreach (var v in LuaScriptBundle.GetAllAssetNames())
        {
            BTLog.Error("lua:{0}", v);
        }

        BTLog.Error("{0}", LuaScriptBundle.LoadAsset("assets/luatemp/lua/testpool.txt") != null);
#endif
    }
    
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    public static byte[] ReqireLua(string name)
    {
#if !UNITY_EDITOR || USE_BUNDLE
        var realname = name.Replace('/', '.');
        realname = realname.Replace("\\", ".");
        if (LuaScriptBundle != null)
        {
            var txtAsset = LuaScriptBundle.LoadAsset<TextAsset>(realname);
            if (txtAsset == null)
            {
                 txtAsset = ToLuaScriptBundle.LoadAsset<TextAsset>(realname);
                if (txtAsset == null)
                {
                    int index = realname.LastIndexOf(".lua");
                    if (-1 != index)
                    {
                        string newName = realname.Remove(index, 4);
                        txtAsset = LuaScriptBundle.LoadAsset<TextAsset>(newName);
                        if (txtAsset == null)
                        {
                            txtAsset = ToLuaScriptBundle.LoadAsset<TextAsset>(newName);
                            if (txtAsset == null)
                            {
                                BTLog.Error("Lua试图获取一个不存在的文件 " + newName);
                            }
                            else
                            {
                                return txtAsset.bytes;
                            }
                        }
                        else
                        {
                            return txtAsset.bytes;
                        }
                    }
                }

                if (txtAsset == null) return null;
                return txtAsset.bytes;
            }
            else
            {
                return txtAsset.bytes;
            }
        }

        return null;
#else
        byte[] buffer = LuaFileUtils.Instance.ReadFile(name);
        return buffer;
#endif
    }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    public static int MyLoader(IntPtr luaState)
    {
        string module = LuaDLL.lua_tostring(luaState, 1);
        byte[] buffer = ReqireLua(module);
        if (null != buffer)
        {
            LuaDLL.luaL_loadbuffer(luaState, buffer, buffer.Length, module);
        }
        else
        {
            LuaDLL.lua_pushstring(luaState, "\n\tError - MyLoader could not find ");
        }

        return 1;
    }

    private static void AddLoader(LuaState state)
    {
        state.LuaGetGlobal("package");
        state.LuaGetField(-1, "loaders");
        state.LuaRemove(-2);
        int numLoaders = 0;
        state.LuaPushNil();
        while (state.LuaNext(-2))
        {
            state.LuaPop(1);
            numLoaders++;
        }
        state.LuaPushInteger(numLoaders + 1);
        state.LuaPushFunction(MyLoader);
        state.LuaRawSet(-3);
        // Table is still on the stack.  Get rid of it now.
        state.LuaPop(1);
    }
}
