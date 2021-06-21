------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.06_Image.UI_06:Framework.UI.Prefab
---@field m_Image Framework.UI.Image
---@field m_Button Framework.UI.Button
UI_06 = class("Examples.06_Image.UI_06", super)

function UI_06:ctor(autoBind)
    super.ctor(self, autoBind)
	self.m_Image = Image.New()
	self:AddChild(self.m_Image)
	self.m_Button = Button.New()
	self:AddChild(self.m_Button)
end

function UI_06:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/06_Image/UI_06.prefab"
    else
        return "2"
    end
end

return UI_06
