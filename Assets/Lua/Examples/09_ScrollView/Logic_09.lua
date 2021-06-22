require("Examples.09_ScrollView.Item_09")
require("Examples.09_ScrollView.UI_09")


---@class Logic_09
---@field ui Examples.09_ScrollView.UI_09
Logic_09 = class("Logic_09")

function Logic_09:ctor()
    self.ui = UI_09.New()
    for i = 1, 5 do
        local item = Item_09.New()
        local iconName = "item_able"
        if i % 2 == 0 then
            iconName = "item_basic"
        end
        item.icon_Image:SetAtlasImage("Assets/Lua/Examples/09_ScrollView/Arena.png", iconName)
        self.ui.m_SV:AddChild(item)
    end
end

return Logic_09