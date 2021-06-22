------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.09_ScrollView.Item_09:Framework.UI.Prefab
---@field icon_Image Framework.UI.Image
Item_09 = class("Examples.09_ScrollView.Item_09", super)

function Item_09:ctor(autoBind)
    super.ctor(self, autoBind)
	self.icon_Image = Image.New()
	self:AddChild(self.icon_Image)
end

function Item_09:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/09_ScrollView/Item_09.prefab"
    else
        return "2"
    end
end

return Item_09
