------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.01_SimpleUI.UI_01:Framework.UI.Prefab
UI_01 = class("Examples.01_SimpleUI.UI_01", super)

function UI_01:ctor(autoBind)
    super.ctor(self, autoBind)
end

function UI_01:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/01_SimpleUI/UI_01.prefab"
    else
        return "2"
    end
end

return UI_01
