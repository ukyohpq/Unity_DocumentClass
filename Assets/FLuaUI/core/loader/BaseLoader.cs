using LuaInterface;

namespace FLuaUI.core.loader
{
    public class BaseLoader
    {
        public LuaTable lt;

        public BaseLoader(LuaTable lt)
        {
            this.lt = lt;
        }

        public bool HasLT(LuaTable lt)
        {
            return this.lt == lt;
        }
        public virtual void Load()
        {
        }
    }
}