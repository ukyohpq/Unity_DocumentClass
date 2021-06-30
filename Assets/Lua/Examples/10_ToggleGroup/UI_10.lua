------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.10_ToggleGroup.UI_10:Framework.UI.Prefab
---@field Button Framework.UI.Button
UI_10 = class("Examples.10_ToggleGroup.UI_10", super)

function UI_10:ctor(autoBind)
    super.ctor(self, autoBind)
	self.Button = Button.New()
	self:AddChild(self.Button)
end

function UI_10:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/10_ToggleGroup/UI_10.prefab"
    else
        return "2"
    end
end

return UI_10
