----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.04_Button.UI_04:Framework.UI.Prefab
---@field m_Button Framework.UI.Button
---@field m_Text Framework.UI.TextField
UI_04 = class("Examples.04_Button.UI_04", super)

function UI_04:ctor(autoBind)
    super.ctor(self)
	self.m_Button = Button.New()
	self:InsertChild(self.m_Button)
	self.m_Text = TextField.New()
	self:InsertChild(self.m_Text)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function UI_04:GetAssetPath()
    return "Assets/UI/Prefab/Example/UI_04.prefab"
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return UI_04
