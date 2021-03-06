----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.06_Image.UI_06:Framework.UI.Prefab
---@field m_Image Framework.UI.Image
---@field m_Button Framework.UI.Button
UI_06 = class("Examples.06_Image.UI_06", super)

function UI_06:ctor(autoBind)
    super.ctor(self)
	self.m_Image = Image.New()
	self:InsertChild(self.m_Image)
	self.m_Button = Button.New()
	self:InsertChild(self.m_Button)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function UI_06:GetAssetPath()
    return "Assets/UI/Prefab/Example/UI_06.prefab"
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return UI_06
