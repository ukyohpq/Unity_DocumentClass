----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.04_Button.UI_04:Framework.UI.Prefab
---@field m_Button Framework.UI.Button
---@field m_Text Framework.UI.TextField
UI_04 = class("Examples.04_Button.UI_04", super)

function UI_04:ctor(autoBind)
    super.ctor(self, autoBind)
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

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return UI_04
