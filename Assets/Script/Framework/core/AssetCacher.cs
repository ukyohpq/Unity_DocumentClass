using UnityEngine;

namespace Framework.core
{
    public class AssetCacher
    {
        private string path;
        private int numRef;
        private GameObject asset;
        private float updateTime = Time.time;

        public AssetCacher(string path)
        {
            this.path = path;
            numRef = 0;
        }

        public GameObject GetAsset()
        {
            numRef++;
            return GameObject.Instantiate(asset);
        }

        public bool Check()
        {
            return numRef > 0 && (Time.time - updateTime < 10);
        }
    }
}