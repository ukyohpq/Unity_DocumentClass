------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.05_DocumentClass.UI_Doc:Framework.UI.Prefab
---@field m_Button Framework.UI.Button
---@field m_Text Framework.UI.TextField
UI_Doc = class("Examples.05_DocumentClass.UI_Doc", super)

function UI_Doc:ctor(autoBind)
    super.ctor(self, autoBind)
	self.m_Button = Button.New()
	self:AddChild(self.m_Button)
	self.m_Text = TextField.New()
	self:AddChild(self.m_Text)
end

function UI_Doc:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/05_DocumentClass/Doc.prefab"
    else
        return "2"
    end
end

return UI_Doc
