------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.03_Text.UI_03:Framework.UI.Prefab
---@field m_Text Framework.UI.TextField
UI_03 = class("Examples.03_Text.UI_03", super)

function UI_03:ctor(autoBind)
    super.ctor(self, autoBind)
	self.m_Text = TextField.New()
	self:AddChild(self.m_Text)
end

function UI_03:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/03_Text/UI_03.prefab"
    else
        return "2"
    end
end

return UI_03
