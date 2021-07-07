require("Examples.04_Button.UI_04")
require("Framework.event.Event")

---@class Examples.04_Button.logic_04
---@field ui Examples.04_Button.UI_04
---@field numClick number
logic_04 = class("Examples.04_Button.logic_04")

function logic_04:ctor()
    self.numClick = 0
    self.ui = UI_04.New()
    self.ui.m_Button.EventClick:Add(self, self.onClick)
    --self.ui.m_Button:SetActive(false)
end

---onClick
---@param evt Framework.event.Event
function logic_04:onClick(evt)
    self.numClick = self.numClick + 1
    self.ui.m_Text:SetText("total click" .. self.numClick .. " target is:" .. evt.target.__cname)
    --local ui = self.ui
    --self.ui = nil
    --ui:Destroy()
end

return logic_04