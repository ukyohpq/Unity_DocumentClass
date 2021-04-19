require("Examples.05_DocumentClass.UI_05")
require("Framework.event.Event")

---@class Examples.05_DocumentClass.logic
---@field ui Examples.05_DocumentClass.UI_05
---@field numClick number
logic = class("Examples.05_DocumentClass.logic")

function logic:ctor()
    self.numClick = 0
    self.ui = UI_05.New()
    self.ui:LoadResource()
    self.ui:AddEventListener(Event.COMPLETE, self, self.onComplete)
end

---onComplete
---@param evt Framework.event.Event
function logic:onComplete(evt)
    self.ui.m_Doc.m_Button:AddEventListener("click", self, self.onClick)
end

---onClick
---@param evt Framework.event.Event
function logic:onClick(evt)
    self.numClick = self.numClick + 1
    self.ui.m_Doc.m_Text.text = "total click" .. self.numClick
    --local ui = self.ui
    --self.ui = nil
    --ui:Destroy()
end

return logic