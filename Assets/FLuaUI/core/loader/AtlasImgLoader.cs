using Babeltime.Log;
using LuaInterface;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace FLuaUI.core.loader
{
    public class AtlasImgLoader:BaseLoader
    {
        private string atlas;
        private string name;
        public AtlasImgLoader(LuaTable lt, string atlas, string name):base(lt)
        {
            this.atlas = atlas;
            this.name = name;
        }

        public override void Load()
        {
            var texs = AssetDatabase.LoadAllAssetsAtPath(atlas);
            if (texs == null)
            {
                BTLog.Error("can not find atlas:{0}", atlas);
                return;
            }

            Sprite t = null;
            for (int i = 0; i < texs.Length; i++)
            {
                if (texs[i].name == name)
                {
                    t = texs[i] as Sprite;
                }
            }

            if (t == null)
            {
                BTLog.Error("can not find sprite:{0} in atlas:{1}", name, atlas);
                return;
            }
            var ls = lt.GetLuaState();
            lt.Push();
            ls.LuaPushValue(-1);
            ls.LuaGetTable(LuaIndexes.LUA_REGISTRYINDEX);
            var binder = ls.ToVariant(-1) as MonoBehaviour;
            var unityImage = binder.gameObject.GetComponent<Image>(); 
            unityImage.sprite = t;
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