namespace FLua.Log
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using DateTime = System.DateTime;
    using Exception = System.Exception;
    using Out = UnityEngine.Debug;

    public enum LogLevel : int
    {
        Debug = 0,
        Info = 1,
        Warning = 2,
        Error = 3,
        Exception = 4,
        None = 5,
    }

    public delegate void UploadMessage(string message);

    public delegate void PrintFunc(string message, Object context);

    public static class BTLog
    {
        public static int level = 0;
        public static bool OnLineDebugInfoOpen = false;

        //用于对接SDK的上传日志功能
        [LuaInterface.NoToLua]
        public static UploadMessage upload;

        //一个静态的StringBuilder，用于组装输出日志
        private static StringBuilder s_text;

        private static int mainThreadID;
        public static bool IsMainThread
        {
            get
            {
                return mainThreadID == System.Threading.Thread.CurrentThread.ManagedThreadId;
            }
        }

        //存储什么日志类型需要上传
        private static Dictionary<LogType, bool> s_typesToUpload = new Dictionary<LogType, bool>()
        {
            { LogType.Log, false },
            { LogType.Warning, false },
            { LogType.Error, false },
            { LogType.Exception, false },
        };

        private const string TIME_FMT = "HH:mm:ss.fff";

        //不同LOG类型的前缀标签
#if !UNITY_EDITOR
        private const string LABEL_DEBUG        = "[DEBUG_INFO]";
        private const string LABEL_INFO         = "[INFO]";
        private const string LABEL_WARNING      = "[WARNING]";
        private const string LABEL_ERROR        = "[ERROR]";
#else
        //在Unity的编辑器控制台里加一些颜色信息
        private const string LABEL_DEBUG = "<color=lime>[DEBUG_INFO]</color>";
        private const string LABEL_INFO = "<color=olive>[INFO]</color>";
        private const string LABEL_WARNING = "<color=yellow>[WARNING]</color>";
        private const string LABEL_ERROR = "<color=red>[ERROR]</color>";
#endif
        [LuaInterface.NoToLua]
        public static void Init()
        {
            mainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
        }
        private static StringBuilder text
        {
            get
            {
                if (s_text == null)
                {
                    s_text = new StringBuilder();
                }

                return s_text;
            }
        }

        // 添加时间和帧数显示
        private static StringBuilder Prefix(string label)
        {
            var t = new StringBuilder();
#if UNITY_EDITOR
            t.Append('[');
            t.Append("<color=teal>");
            t.Append(DateTime.Now.ToString(TIME_FMT));
            t.Append("</color>");
            t.Append("] ");
            if (IsMainThread)
            {
                t.AppendFormat("[{0},{1}]", Time.frameCount, Time.unscaledDeltaTime);
            }
#else
          
#endif
            t.Append(label);
            t.Append(' ');
            return t;
        }

        private static bool Convert(LogLevel logLevel, out string label, out LogType logType, out PrintFunc print)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    print = Out.Log;
                    label = LABEL_DEBUG;
                    logType = LogType.Log;
                    return true;
                case LogLevel.Info:
                    print = Out.Log;
                    label = LABEL_INFO;
                    logType = LogType.Log;
                    return true;
                case LogLevel.Warning:
                    print = Out.LogWarning;
                    label = LABEL_WARNING;
                    logType = LogType.Warning;
                    return true;
                case LogLevel.Error:
                    print = Out.LogError;
                    label = LABEL_ERROR;
                    logType = LogType.Error;
                    return true;
                default:
                    print = null;
                    label = null;
                    logType = LogType.Log;
                    return false;
            }
        }

        private static void PrintMsg(LogLevel logLevel, bool notrace, string message, Object context)
        {
            string label;
            LogType logType;
            PrintFunc print;
            if (!Convert(logLevel, out label, out logType, out print))
            {
                return;
            }

            var t = Prefix(label);
            t.Append(message);
            var m = t.ToString();
            if (notrace)
            {
                var stackTraceLogType = Application.GetStackTraceLogType(logType);
                Application.SetStackTraceLogType(logType, StackTraceLogType.None);
                print(m, context);
                Application.SetStackTraceLogType(logType, stackTraceLogType);
            }
            else
            {
                print(m, context);
            }
            if (upload == null)
            {
                return;
            }

            bool needUpload;
            if (s_typesToUpload.TryGetValue(logType, out needUpload) && needUpload)
            {
                upload(m);
            }
        }

        private static void PrintFmt(LogLevel logLevel, bool notrace, string format, params object[] args)
        {
            string label;
            LogType logType;
            PrintFunc print;
            if (!Convert(logLevel, out label, out logType, out print))
            {
                return;
            }

            var t = Prefix(label);
            t.AppendFormat(format, args);
            var m = t.ToString();
            t.Remove(0, t.Length);
            if (notrace)
            {
                var stackTraceLogType = Application.GetStackTraceLogType(logType);
                Application.SetStackTraceLogType(logType, StackTraceLogType.None);
                print(m, null);
                Application.SetStackTraceLogType(logType, stackTraceLogType);
            }
            else
            {
                print(m, null);
            }
            if (upload == null)
            {
                return;
            }

            bool needUpload;
            if (s_typesToUpload.TryGetValue(logType, out needUpload) && needUpload)
            {
                upload(m);
            }
        }

        private static bool IsMuted(LogLevel logLevel)
        {
            return (int)logLevel < level;
        }

        [LuaInterface.NoToLua]
        public static bool GetLogTypeToUpload(LogType logType)
        {
            bool value;
            if (s_typesToUpload.TryGetValue(logType, out value))
            {
                return value;
            }

            return false;
        }

        [LuaInterface.NoToLua]
        public static void SetLogTypeToUpload(LogType logType, bool upload)
        {
            if (s_typesToUpload.ContainsKey(logType))
            {
                s_typesToUpload[logType] = upload;
            }
            else
            {
                s_typesToUpload.Add(logType, upload);
            }
        }


        #region Debug
        [LuaInterface.NoToLua]
        public static void Debug(string message, Object context = null)
        {
            if (IsMuted(LogLevel.Debug))
            {
                return;
            }

            PrintMsg(LogLevel.Debug, false, message, context);
        }
        [LuaInterface.NoToLua]
        public static void Debug(string format, params object[] args)
        {
            if (IsMuted(LogLevel.Debug))
            {
                return;
            }

            PrintFmt(LogLevel.Debug, false, format, args);
        }

        public static void D(string message)
        {
            if (IsMuted(LogLevel.Debug))
            {
                return;
            }

            PrintMsg(LogLevel.Debug, false, message, null);
        }



        #endregion

        #region Info
        [LuaInterface.NoToLua]
        public static void Info(string message, Object context = null)
        {
            if (IsMuted(LogLevel.Info))
            {
                return;
            }

            PrintMsg(LogLevel.Info, false, message, context);
        }
        [LuaInterface.NoToLua]
        public static void Info(string format, params object[] args)
        {
            if (IsMuted(LogLevel.Info))
            {
                return;
            }

            PrintFmt(LogLevel.Info, false, format, args);
        }

        public static void I(string message)
        {
            if (IsMuted(LogLevel.Info))
            {
                return;
            }

            PrintMsg(LogLevel.Info, false, message, null);
        }
        #endregion

        [LuaInterface.NoToLua]
        public static void OnLineDebugInfo(string format, params object[] args)
        {

#if !UNITY_EDITOR
            if (!OnLineDebugInfoOpen)
            {
                return;
            }
           PrintFmt(LogLevel.Info, false, format, args);   
#endif
        }

        public static void OnLineInfo(string message)
        {
#if !UNITY_EDITOR
            if (!OnLineDebugInfoOpen)
            {
                return;
            }
            PrintMsg(LogLevel.Info, false, message, null);
#endif
        }

        public static void LogNetDebugInfo(string format, params object[] args)
        {
//#if !UNITY_EDITOR
            if ((int)level < 5)
            {
                return;
            }
            PrintFmt(LogLevel.Info, false, format, args);
        }



        #region Warning
        [LuaInterface.NoToLua]
        public static void Warning(string message, Object context = null)
        {
            if (IsMuted(LogLevel.Warning))
            {
                return;
            }

            PrintMsg(LogLevel.Warning, false, message, context);
        }
        [LuaInterface.NoToLua]
        public static void Warning(string format, params object[] args)
        {
            if (IsMuted(LogLevel.Warning))
            {
                return;
            }

            PrintFmt(LogLevel.Warning, false, format, args);
        }

        public static void W(string message)
        {
            if (IsMuted(LogLevel.Warning))
            {
                return;
            }

            PrintMsg(LogLevel.Warning, true, message, null);
        }
#endregion

#region Error
        [LuaInterface.NoToLua]
        public static void Error(string message, Object context = null)
        {
            if (IsMuted(LogLevel.Error))
            {
                return;
            }

            PrintMsg(LogLevel.Error, false, message, context);
        }
        [LuaInterface.NoToLua]
        public static void Error(string format, params object[] args)
        {
            if (IsMuted(LogLevel.Error))
            {
                return;
            }

            PrintFmt(LogLevel.Error, false, format, args);
        }

        public static void E(string message)
        {
            if (IsMuted(LogLevel.Error))
            {
                return;
            }

            PrintMsg(LogLevel.Error, true, message, null);
        }
#endregion

#region Exception
        [LuaInterface.NoToLua]
        public static void Exception(Exception exception)
        {
            if (IsMuted(LogLevel.Exception))
            {
                return;
            }

            Out.LogException(exception);
            if (upload == null)
            {
                return;
            }

            bool needUpload;
            if (s_typesToUpload.TryGetValue(LogType.Exception, out needUpload) && needUpload)
            {
                upload(exception.ToString());
            }
        }
#endregion

        public static void Exception(string message, Object context = null)
        {
            if (IsMuted(LogLevel.Exception))
            {
                return;
            }

            var t = Prefix("");
            t.Append(message);
            Out.LogException(new Exception(t.ToString()));
        }


        public static int GetLogLevelMacro()
        {
#if GAME_LOG_LEVEL_0
            return 0;
#endif
#if GAME_LOG_LEVEL_1
            return 1;
#endif
#if GAME_LOG_LEVEL_2
            return 2;
#endif
#if GAME_LOG_LEVEL_3
            return 3;
#endif
#if GAME_LOG_LEVEL_4
            return 4;
#endif
#if GAME_LOG_LEVEL_5
            return 5;
#endif
            return (int)LogLevel.Debug;
        }
    }
}