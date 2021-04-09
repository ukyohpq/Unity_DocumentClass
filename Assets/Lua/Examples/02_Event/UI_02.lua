------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.02_Event.UI_02:Framework.UI.Prefab
UI_02 = class("Examples.02_Event.UI_02", super)

function UI_02:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/02_Event/UI_02.prefab"
    else
        return "2"
    end
end

return UI_02
