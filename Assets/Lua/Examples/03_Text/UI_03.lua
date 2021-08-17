----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.03_Text.UI_03:Framework.UI.Prefab
---@field m_Text Framework.UI.TextField
UI_03 = class("Examples.03_Text.UI_03", super)

function UI_03:ctor(autoBind)
    super.ctor(self)
	self.m_Text = TextField.New()
	self:AddChild(self.m_Text)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function UI_03:GetAssetPath()
    if IsEditor then
        return "Assets/UI/Prefab/Example/UI_03.prefab"
    else
        return "2"
    end
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return UI_03
