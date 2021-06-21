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
---@field m_Text Framework.UI.TextField
UI_04 = class("Examples.04_Button.UI_04", super)

function UI_04:ctor()
    super.ctor(self)
	self.m_Button = Button.New()
	self:AddChild(self.m_Button)
	self.m_Text = TextField.New()
	self:AddChild(self.m_Text)
end

function UI_04:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/04_Button/UI_04.prefab"
    else
        return "2"
    end
end

return UI_04
