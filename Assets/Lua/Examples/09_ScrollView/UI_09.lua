----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.09_ScrollView.UI_09:Framework.UI.Prefab
---@field m_SV Framework.UI.ScrollView
UI_09 = class("Examples.09_ScrollView.UI_09", super)

function UI_09:ctor(autoBind)
    super.ctor(self)
	self.m_SV = ScrollView.New()
	self:AddChild(self.m_SV)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function UI_09:GetAssetPath()
    return "Assets/UI/Prefab/Example/UI_09.prefab"
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return UI_09
