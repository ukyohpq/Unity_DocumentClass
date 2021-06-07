using System.Collections.Generic;
using Framework.core.loader;
using LuaInterface;

namespace Framework.core
{
    public class CSBridge
    {
        private static List<BaseLoader> loaderContexts = new List<BaseLoader>();
        public static void LoadPrefab(LuaTable lt)
        {
            loaderContexts.Add(new PrefabLoader(lt));
        }

        public static void LoadImage(LuaTable lt, string path) 
        {
            loaderContexts.Add(new ImageLoader(lt, path));
        }
        
        public static void StopLoad(LuaTable lb)
        {
            if (loaderContexts.Count == 0) return;
            foreach (var lc in loaderContexts)
            {
                if (lc.HasLT(lb))
                {
                    loaderContexts.Remove(lc);
                    return;
                }
            }
        }
        
        [NoToLua]
        public static void LoadAsset()
        {
            for (int i = 0; i < 10; i++)
            {
                if (loaderContexts.Count == 0) return;
                var context = loaderContexts[0];
                loaderContexts.RemoveAt(0);
                context.Load();
            }
            
        }
    }
}