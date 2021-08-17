using System;
using Babeltime.Log;
using FLuaUI.LuaUI.Components;
using LuaInterface;
using UnityEditor;
using UnityEngine;

namespace FLuaUI.core.loader
{
    public class PrefabLoader:BaseLoader
    {
        public PrefabLoader(LuaTable lt):base(lt)
        {
            
        }
        public override void Load()
        {
            var fc = lt["GetAssetPath"] as LuaFunction;
            var path = fc.Invoke<string>();
            var prefab = LoaderManager.AssetsAPI.LoadPrefab(path);
            if (prefab == null)
            {
                BTLog.Error("can not find prefab in path:{0}", path);
                return;
            }

            var go = GameObject.Instantiate(prefab);
            MainGame.Ins.AddChild2Stage(go);
            go.transform.localPosition = Vector3.zero;
                
            var docu = go.GetComponent<DocumentClass>();
            if (docu == null)
            {
                throw new Exception("must has Component DocumentClass!");
            }
            docu.SetLuaTable(lt);
        }
    }
}