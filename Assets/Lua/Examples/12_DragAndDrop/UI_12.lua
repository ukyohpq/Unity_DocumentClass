----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.12_DragAndDrop.UI_12:Framework.UI.Prefab
---@field Image Framework.UI.Image
---@field Image_1 Framework.UI.Image
UI_12 = class("Examples.12_DragAndDrop.UI_12", super)

function UI_12:ctor(autoBind)
    super.ctor(self)
	self.Image = Image.New()
	self:AddChild(self.Image)
	self.Image_1 = Image.New()
	self:AddChild(self.Image_1)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function UI_12:GetAssetPath()
    if IsEditor then
        return "Assets/UI/Prefab/Example/UI_12.prefab"
    else
        return "2"
    end
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return UI_12
