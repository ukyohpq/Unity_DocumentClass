------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.04_Button.UI_04:Framework.UI.Prefab
---@field m_Button Framework.UI.Button
---@field m_Text UnityEngine.UI.Text
UI_04 = class("Examples.04_Button.UI_04", super)

function UI_04:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/04_Button/UI_04.prefab"
    else
        return "2"
    end
end

return UI_04
