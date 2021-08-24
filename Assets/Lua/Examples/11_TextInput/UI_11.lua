----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.11_TextInput.UI_11:Framework.UI.Prefab
---@field InputField Framework.UI.InputField
---@field Text Framework.UI.TextField
UI_11 = class("Examples.11_TextInput.UI_11", super)

function UI_11:ctor(autoBind)
    super.ctor(self)
	self.InputField = InputField.New()
	self:InsertChild(self.InputField)
	self.Text = TextField.New()
	self:InsertChild(self.Text)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function UI_11:GetAssetPath()
    return "Assets/UI/Prefab/Example/UI_11.prefab"
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return UI_11
