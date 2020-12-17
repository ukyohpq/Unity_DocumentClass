/**
 * Created by ukyohpq.
 * DateTime: 17/12/26 11:50
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using LuaInterface;

public class HotFixLua : MonoBehaviour {
    public string LuaPath;
    private Thread rluaTread;
    private List<string> luaModiQue;
	// Use this for initialization
	void Start () {
        luaModiQue = new List<string>();
        rluaTread = new Thread(checkLuaChange);
        rluaTread.Start(Application.dataPath + LuaPath);
	}
	
    private void checkLuaChange(object param)
    {
        string luaPath = param.ToString();
        var luaDirInfo = new System.IO.DirectoryInfo(luaPath);
        var luaFiles = new Dictionary<string, System.DateTime>();
        foreach(var file in luaDirInfo.GetFiles("*.lua", System.IO.SearchOption.AllDirectories))
        {
            luaFiles[file.FullName] = file.LastWriteTime;
        }
        while(true)
        {
            foreach(var file in luaDirInfo.GetFiles("*.lua", System.IO.SearchOption.AllDirectories))
            {
                if(!luaFiles.ContainsKey(file.FullName))
                {
                    luaFiles[file.FullName] = file.LastWriteTime;
                    lock (luaModiQue)
                    {
                        luaModiQue.Add(file.FullName); 
                    }
                }
                else if(!System.DateTime.Equals(luaFiles[file.FullName], file.LastWriteTime))
                {
                    luaFiles[file.FullName] = file.LastWriteTime;
                    var luaFilePath = file.FullName;
                    if(luaFilePath.IndexOf("\\") != -1)
                    {
                        luaFilePath = luaFilePath.Replace(".lua", "");
                        luaFilePath = luaFilePath.Replace("\\", ".");
                        luaPath = luaPath.Replace("/", ".");
                        luaFilePath = luaFilePath.Replace(luaPath, "");
                    }else{
                        luaFilePath = luaFilePath.Replace(luaPath, "");
                        luaFilePath = luaFilePath.Replace(".lua", "");
                        luaFilePath = luaFilePath.Replace("/", ".");
                    }
                    lock(luaModiQue)
                    {
                        luaModiQue.Add(luaFilePath);
                    }
                }
            }
            System.Threading.Thread.Sleep(1000);
        }
    }

	// Update is called once per frame
	void Update () {
        string luaFilePath = "";
        lock(luaModiQue)
        {
            if(luaModiQue.Count > 0)
            {
                luaFilePath = luaModiQue[0];
                luaModiQue.RemoveAt(0);
            }
        }
        if(!string.IsNullOrEmpty(luaFilePath))
        {
            var scr = "luaReload(\"" + luaFilePath + "\")";
//                            var scr = "local luaPath = '" + luaFilePath + "'\n" +
//                                "if package.loaded[luaPath] ~= nil then package.loaded[luaPath] = nil\nrequire(luaPath) " +
//                                "LogUtil.Log('%s reloaded success', luaPath)" +
//                                "else LogUtil.LogError('this path of lua is not loaded. %s', luaPath) end";
            LuaState.Get(IntPtr.Zero).DoString(scr);
        }
	}
}
