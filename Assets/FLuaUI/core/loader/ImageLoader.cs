using Babeltime.Log;
using FLuaUI.LuaUI.Components;
using LuaInterface;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FLuaUI.core.loader
{
    public class ImageLoader:BaseLoader
    {
        private string path;
        public ImageLoader(LuaTable lt, string path):base(lt)
        {
            this.path = path;
        }
        public override void Load()
        {
            var t = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            var ls = lt.GetLuaState();
            lt.Push();
            ls.LuaPushValue(-1);
            ls.LuaGetTable(LuaIndexes.LUA_REGISTRYINDEX);
            var binder = ls.ToVariant(-1) as FLuaImage;
            var unityImage = binder.gameObject.GetComponent<Image>();
            unityImage.sprite = t;
            if (binder.AutoNativeSize)
            {
                unityImage.SetNativeSize();
            }
            ls.LuaPop(1);
            ls.LuaGetField(-1, "DispatchMessage");
            if (ls.LuaIsNil(-1))
            {
                ls.LuaPop(1);
                BTLog.Warning("Prefab Lua must has Method:DispatchMessage");
                return;
            }
            ls.LuaInsert(-2);
            ls.Push("COMPLETE");
            ls.LuaSafeCall(2, 0, 0, 0);
        }
    }
}