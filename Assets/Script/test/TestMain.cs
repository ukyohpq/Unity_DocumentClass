using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using FLua.Log;
using UnityEngine;
using UnityEngine.UI;

public class TestMain : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text += String.Format("streamingAssetsPath:{0}\n persistentDataPath:{1}\n dataPath:{2}\n consoleLogPath:{3}\n temporaryCachePath:{4}", Application.streamingAssetsPath, Application.persistentDataPath, Application.dataPath, Application.consoleLogPath, Application.temporaryCachePath);

        try
        {
            writeTest(Application.streamingAssetsPath, "streamingAssetsPath");
        }
        catch (Exception e)
        {
            text.text += string.Format("\n" + e.Message);
        }

        try
        {
            writeTest(Application.persistentDataPath, "persistentDataPath");
        }
        catch (Exception e)
        {
            text.text += string.Format("\n" + e.Message);
        }


        try
        {
            writeTest(Application.dataPath, "dataPath");
        }
        catch (Exception e)
        {
            text.text += string.Format("\n" + e.Message);
        }

        try
        {
            writeTest(Regex.Replace(Application.consoleLogPath.Substring(0, Application.consoleLogPath.LastIndexOf("/")), @"\\\w+\.log", ""), "consoleLogPath");
        }
        catch (Exception e)
        {
            text.text += string.Format("\n" + e.Message);
        }


        try
        {
            writeTest(Application.temporaryCachePath, "temporaryCachePath");
        }
        catch (Exception e)
        {
            text.text += string.Format("\n" + e.Message);
        }

    }

    void writeTest(string path, string name)
    {

        text.text += string.Format("\ntry write:{0}", name);
        var file = File.OpenWrite(path + "/a.txt");
        file.WriteByte(24);
        file.Close();
        text.text += string.Format("\nwrite {0} success.", name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
