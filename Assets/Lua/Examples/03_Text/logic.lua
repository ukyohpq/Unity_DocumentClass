require("Examples.03_Text.UI_03")
require("Framework.event.Event")

---@class Examples.03_Text.logic_03
---@field ui Examples.03_Text.UI_03
logic_03 = class("Examples.03_Text.logic_03")

function logic_03:ctor()
    self.ui = UI_03.New()
    self.ui.m_Text:SetText("Hello world")
end

return logic_03