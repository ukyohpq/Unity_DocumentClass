using System;
using System.Collections.Generic;
using System.IO;
using FLua.Log;
using LuaInterface;
using UnityEngine;

namespace FLuaUI
{
//    用FileSystemWatcher实现的文件变化处理，目前还未完成，需要多线程处理
    public class HotFixLua:MonoBehaviour
    {
        public string LuaPath;
        private string luaFullPath;
        private List<string> luaModiQue;
        private void Start()
        {
            luaModiQue = new List<string>();
            var watcher = new FileSystemWatcher();
            watcher.BeginInit();
            watcher.Filter = "*.lua";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            watcher.NotifyFilter = NotifyFilters.LastWrite| NotifyFilters.Size;
            watcher.Path = Application.dataPath + LuaPath;
            watcher.Changed += new FileSystemEventHandler(OnCfgFileChange);
            watcher.EndInit();
            luaFullPath = Application.dataPath + LuaPath;
        }

        private void OnCfgFileChange(object sender, FileSystemEventArgs e)
        {
            var luaPath = luaFullPath;
            var luaFilePath = e.FullPath;
            if (luaFilePath.IndexOf("\\") != -1)
            {
                luaFilePath = luaFilePath.Replace(".lua", "");
                luaFilePath = luaFilePath.Replace("\\", ".");
                luaPath = luaPath.Replace("/", ".");
                luaFilePath = luaFilePath.Replace(luaPath, "");
            }
            else
            {
                luaFilePath = luaFilePath.Replace(luaPath, "");
                luaFilePath = luaFilePath.Replace(".lua", "");
                luaFilePath = luaFilePath.Replace("/", ".");
            }
//            BTLog.Error("hot2 OnCfgFileChange:{0} LuaPath:{1}", luaFilePath, luaPath);
            lock (luaModiQue)
            {
                luaModiQue.Add(luaFilePath);
            }
        }
        
        void Update()
        {
            string luaFilePath = "";
            lock (luaModiQue)
            {
                if (luaModiQue.Count > 0)
                {
                    luaFilePath = luaModiQue[0];
                    luaModiQue.RemoveAt(0);
                }
            }

            if (!string.IsNullOrEmpty(luaFilePath))
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
}