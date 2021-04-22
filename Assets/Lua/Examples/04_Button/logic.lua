require("Examples.04_Button.UI_04")
require("Framework.event.Event")

---@class Examples.04_Button.logic
---@field ui Examples.04_Button.UI_04
---@field numClick number
logic = class("Examples.04_Button.logic")

function logic:ctor()
    self.numClick = 0
    self.ui = UI_04.New()
    self.ui:AddEventListener(Event.COMPLETE, self, self.onComplete)
end

---onComplete
---@param evt Framework.event.Event
function logic:onComplete(evt)
    self.ui.m_Button:AddEventListener("click", self, self.onClick)
end

---onClick
---@param evt Framework.event.Event
function logic:onClick(evt)
    self.numClick = self.numClick + 1
    self.ui.m_Text.text = "total click" .. self.numClick .. " target is:" .. evt.target.__cname
    --local ui = self.ui
    --self.ui = nil
    --ui:Destroy()
end

return logic