------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.09_ScrollView.UI_09:Framework.UI.Prefab
---@field m_SV Framework.UI.ScrollView
UI_09 = class("Examples.09_ScrollView.UI_09", super)

function UI_09:ctor(autoBind)
    super.ctor(self, autoBind)
	self.m_SV = ScrollView.New()
	self:AddChild(self.m_SV)
end

function UI_09:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/09_ScrollView/UI_09.prefab"
    else
        return "2"
    end
end

return UI_09
