using Framework.core.Components;
using UnityEngine.UI;

namespace Script.Framework.LuaUI.Components
{
    public class FLuaInputField:GameObjectLuaBinder
    {
        private void Awake()
        {
            var input = gameObject.GetComponent<InputField>();
            input.onEndEdit.AddListener(OnEndEdit);
        }

        private void OnEndEdit(string text)
        {
            DispatchEvent("OnEndEdit");
        }

        private void DispatchEvent(string evtName)
        {
            var ls = GetLuaState();
            PushLuaTable();
            ls.LuaGetField(-1, "DispatchMessage");
            ls.LuaInsert(-2);
            ls.LuaPushString(evtName);
            var input = gameObject.GetComponent<InputField>();
            ls.LuaPushString(input.text);
            ls.LuaSafeCall(3, 0, 0, 0);
        }
        public override string GetLuaClassName()
        {
            return "Framework.UI.InputField";
        }
    }
}