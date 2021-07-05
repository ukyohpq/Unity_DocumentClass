----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.09_ScrollView.Item_09:Framework.UI.Prefab
---@field icon_Image Framework.UI.Image
Item_09 = class("Examples.09_ScrollView.Item_09", super)

function Item_09:ctor(autoBind)
    super.ctor(self)
	self.icon_Image = Image.New()
	self:AddChild(self.icon_Image)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function Item_09:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/09_ScrollView/Item_09.prefab"
    else
        return "2"
    end
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return Item_09
