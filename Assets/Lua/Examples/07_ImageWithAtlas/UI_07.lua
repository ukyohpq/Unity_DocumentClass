----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.07_ImageWithAtlas.UI_07:Framework.UI.Prefab
---@field m_Image Framework.UI.Image
---@field m_Button Framework.UI.Button
UI_07 = class("Examples.07_ImageWithAtlas.UI_07", super)

function UI_07:ctor(autoBind)
    super.ctor(self)
	self.m_Image = Image.New()
	self:InsertChild(self.m_Image)
	self.m_Button = Button.New()
	self:InsertChild(self.m_Button)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function UI_07:GetAssetPath()
    return "Assets/UI/Prefab/Example/UI_07.prefab"
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return UI_07
