using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
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
            var platform = "";
            try
            {
                #if UNITY_EDITOR
                    platform = "editor";
//                    var asset2 = File.ReadAllText(Application.streamingAssetsPath + "/datainstreamingAssets.txt");
                #elif UNITY_ANDROID
                    platform = "android";
//                    var asset2 = File.ReadAllText(Application.streamingAssetsPath + "/datainstreamingAssets.txt");
                #elif UNITY_EDITOR_OSX
                    platform = "ios";
//                    var asset2 = File.ReadAllText(Application.streamingAssetsPath + "/datainstreamingAssets.txt");
                #endif
                var path = Path.Combine(Application.streamingAssetsPath, "datainstreamingAssets.txt");
                WWW reader = new WWW(path);
                while (!reader.isDone)
                {
                    
                }
//                var ret = File.Exists(Application.streamingAssetsPath);
//                text.text = string.Format("\nplatform:{0}\npath:{1}\nret:{2}", platform, Application.streamingAssetsPath, ret);
                
                text.text += string.Format("\n {0} path:{1} from Streaming:{2}", platform, path, reader.text);
            }
            catch (Exception e)
            {
                text.text += string.Format("\n{0} err:{1}", platform,  e.Message);
            }
            
        }
    }
}