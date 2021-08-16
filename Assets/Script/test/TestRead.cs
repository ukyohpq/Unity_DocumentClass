using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Script.test
{
    public class TestRead:MonoBehaviour
    {
        public Text text;
        private void Start()
        {
            var asset = Resources.Load<TextAsset>("dataInResources");
            text.text += "from Resource:" + asset.text;

            var asset2 = File.ReadAllText(Application.streamingAssetsPath + "/datainstreamingAssets.txt");
            text.text += "\nfrom Streaming:" + asset2;
        }
    }
}