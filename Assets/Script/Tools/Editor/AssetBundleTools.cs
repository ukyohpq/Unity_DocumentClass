using System;
using System.Collections.Generic;
using FLuaUI.Components;
using UnityEditor;
using UnityEngine;

namespace Script.Tools.Editor
{
    public class AssetBundleTools
    {
        [MenuItem("Tools/AssetBundle/BuildAll")]
        private static void BuildAll()
        {
            BuildAtlas();
            BuildLua();
            BuildUI();
        }

        [MenuItem("Tools/AssetBundle/BuildAtlas")]
        private static void BuildAtlas()
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [MenuItem("Tools/AssetBundle/BuildLua")]
        private static void BuildLua()
        {
            try
            {
                var luaPath = Application.dataPath + "/Lua";
                var toluaPath = Application.dataPath + "/ToLua";
                var builds = new List<AssetBundleBuild>();
                var luaBuilder = new AssetBundleBuild();
                var toluaBuilder = new AssetBundleBuild();
                builds.Add(luaBuilder);
                builds.Add(toluaBuilder);
                var opertion = BuildAssetBundleOptions.ChunkBasedCompression |
                               BuildAssetBundleOptions.DeterministicAssetBundle;
                BuildPipeline.BuildAssetBundles("", builds.ToArray(),opertion, BuildTarget.Android);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [MenuItem("Tools/AssetBundle/BuildUI")]
        private static void BuildUI()
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}