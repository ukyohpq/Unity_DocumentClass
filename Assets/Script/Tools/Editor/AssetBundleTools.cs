using System;
using System.Collections.Generic;
using System.IO;
using Babeltime.Log;
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
            BTLog.Error("Build All OK.");
        }

        [MenuItem("Tools/AssetBundle/BuildAtlas")]
        private static void BuildAtlas()
        {
            try
            {
                var builds = new List<AssetBundleBuild>();
                var opertion = BuildAssetBundleOptions.ChunkBasedCompression |
                               BuildAssetBundleOptions.DeterministicAssetBundle;
                var path = Application.dataPath + "/UI/Atlas";
                foreach (var directory in Directory.GetDirectories(path))
                {
                    var build = new AssetBundleBuild();
                    build.assetBundleName = "Atlas_" + directory.Replace(path + "\\", "");
                    var fileList = new List<string>();
                    foreach (var filePath in Directory.GetFiles(directory, "*.png", SearchOption.AllDirectories))
                    {
                        fileList.Add(filePath.Replace(Application.dataPath, "Assets"));
                    }

                    build.assetNames = fileList.ToArray();
                    builds.Add(build);
                }
                BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, builds.ToArray(), opertion,
                    BuildTarget.Android);
                AssetDatabase.Refresh();
                BTLog.Error("build Atlas ok.");
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
                var toluaPath = Application.dataPath + "/ToLua/Lua";
                var tempPath = Application.dataPath + "/luatemp";
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
                Directory.CreateDirectory(tempPath);
                var builds = new List<AssetBundleBuild>();
                var luaBuilder = MakeLuaABBuild(luaPath, "luaBundle", tempPath);
                var toluaBuilder = MakeLuaABBuild(toluaPath, "toluaBundle", tempPath);
                builds.Add(luaBuilder);
                builds.Add(toluaBuilder);
                var opertion = BuildAssetBundleOptions.ChunkBasedCompression |
                               BuildAssetBundleOptions.DeterministicAssetBundle;
                AssetDatabase.Refresh();
                BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, builds.ToArray(),opertion, BuildTarget.Android);
                Directory.Delete(tempPath, true);
                AssetDatabase.Refresh();
                BTLog.Error("build lua ok.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static AssetBundleBuild MakeLuaABBuild(string path, string bundleName, string tempPath)
        {
            var builder = new AssetBundleBuild();
            var assetNames = new List<string>();
            var addressableNames = new List<string>();
            foreach (var filePath in Directory.GetFiles(path, "*.lua", SearchOption.AllDirectories))
            {
                var txtPath = tempPath + filePath.Replace(Application.dataPath, "");
                txtPath = txtPath.Replace(".lua", ".txt");
                var directory = txtPath.Substring(0, txtPath.LastIndexOf("\\"));
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.Copy(filePath, txtPath);
                txtPath = txtPath.Replace(Application.dataPath, "Assets");
                assetNames.Add(txtPath);
                var assetNamePath = txtPath.Replace("\\", "/");
                assetNamePath = assetNamePath.Replace("ToLua/", "");
                assetNamePath = assetNamePath.Replace("Assets/luatemp/Lua/", "");
                assetNamePath = assetNamePath.Replace("/", ".");
                addressableNames.Add(assetNamePath);
            }

            builder.assetNames = assetNames.ToArray();
            builder.addressableNames = addressableNames.ToArray();
            builder.assetBundleName = bundleName;
            return builder;
        }
        
        [MenuItem("Tools/AssetBundle/BuildUI")]
        private static void BuildUI()
        {
            try
            {
                var builds = new List<AssetBundleBuild>();
                var opertion = BuildAssetBundleOptions.ChunkBasedCompression |
                               BuildAssetBundleOptions.DeterministicAssetBundle;
                var path = Application.dataPath + "/UI/Prefab";
                foreach (var directory in Directory.GetDirectories(path))
                {
                    var build = new AssetBundleBuild();
                    build.assetBundleName = "UI_" + directory.Replace(path + "\\", "");
                    var fileList = new List<string>();
                    foreach (var filePath in Directory.GetFiles(directory, "*.prefab", SearchOption.AllDirectories))
                    {
                        fileList.Add(filePath.Replace(Application.dataPath, "Assets"));
                    }

                    build.assetNames = fileList.ToArray();
                    builds.Add(build);
                }
                BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, builds.ToArray(), opertion,
                    BuildTarget.Android);
                AssetDatabase.Refresh();
                BTLog.Error("build UI ok.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}