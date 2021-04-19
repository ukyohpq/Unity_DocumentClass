using LuaInterface;

namespace Framework.core
{
    public class LoaderContext
    {
        private string path;

        public string Path
        {
            get { return path; }
        }

        private LuaTable luaTb;

        public LuaTable LuaTb
        {
            get { return luaTb; }
        }

        public LoaderContext(string path, LuaTable luaTb)
        {
            this.path = path;
            this.luaTb = luaTb;
        }
    }
}