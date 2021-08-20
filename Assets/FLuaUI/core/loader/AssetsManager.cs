using System.Collections.Generic;
using UnityEngine;

namespace FLuaUI.core.loader
{
    public static class AssetsManager
    {
        public static Dictionary<string, AssetBundle> Bundles = new Dictionary<string, AssetBundle>();
        public static Dictionary<string, AssetBundleCreateRequest> Requests = new Dictionary<string, AssetBundleCreateRequest>();
        public static Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();
        public static Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
        public static Dictionary<string, Sprite[]> Atlases = new Dictionary<string, Sprite[]>();
    }
}