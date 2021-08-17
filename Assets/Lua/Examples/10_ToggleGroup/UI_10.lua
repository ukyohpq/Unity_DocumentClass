----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.10_ToggleGroup.UI_10:Framework.UI.Prefab
---@field ToggleGroup Framework.UI.ToggleGroup
---@field Toggle1 Framework.UI.Toggle
---@field Toggle2 Framework.UI.Toggle
---@field Text Framework.UI.TextField
UI_10 = class("Examples.10_ToggleGroup.UI_10", super)

function UI_10:ctor(autoBind)
    super.ctor(self)
	self.ToggleGroup = ToggleGroup.New()
	self:AddChild(self.ToggleGroup)
	self.Toggle1 = Toggle.New()
	self:AddChild(self.Toggle1)
	self.Toggle2 = Toggle.New()
	self:AddChild(self.Toggle2)
	self.Text = TextField.New()
	self:AddChild(self.Text)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function UI_10:GetAssetPath()
    if IsEditor then
        return "Assets/UI/Prefab/Example/UI_10.prefab"
    else
        return "2"
    end
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
---@param evt Framework.event.Event
function UI_10:OnInit(evt)
    super.OnInit(self, evt)
end

return UI_10
