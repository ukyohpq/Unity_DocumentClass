------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.01_SimpleUI.UI:Framework.UI.Prefab
UI = class("Examples.01_SimpleUI.UI", super)

function UI:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/01_SimpleUI/UI_01.prefab"
    else
        return "2"
    end
end

return UI
