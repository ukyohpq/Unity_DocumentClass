using FLuaUI.Components;
using UnityEngine.UI;

namespace FLuaUI.LuaUI.Components
{
    public class FLuaToggle:GameObjectLuaBinder
    {
        private void Start()
        {
            var toggle = gameObject.GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(onValueChanged);
        }

        private void onValueChanged(bool b)
        {
            var ls = GetLuaState();
            PushLuaTable();
            ls.LuaGetField(-1, "DispatchMessage");
            ls.LuaInsert(-2);
            ls.LuaPushString("ToggleChanged");
            ls.LuaPushBoolean(b);
            ls.LuaSafeCall(3, 0, 0, 0);
            var toggle = gameObject.GetComponent<Toggle>();
            if (toggle.group != null && b)
            {
                var ftg = toggle.group.gameObject.GetComponent<FLuaToggleGroup>();
                ftg.PushLuaTable();
                ls.LuaGetField(-1, "DispatchMessage");
                ls.LuaInsert(-2);
                ls.LuaPushString("ToggleGroupChanged");
                PushLuaTable();
                ls.LuaSafeCall(3, 0, 0, 0);
            }
        }
        public override string GetLuaClassName()
        {
            return "Framework.UI.Toggle";
        }
    }
}