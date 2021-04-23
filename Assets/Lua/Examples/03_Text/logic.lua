require("Examples.03_Text.UI_03")
require("Framework.event.Event")

---@class Examples.03_Text.logic_03
---@field ui Examples.03_Text.UI_03
logic_03 = class("Examples.03_Text.logic_03")

function logic_03:ctor()
    self.ui = UI_03.New()
    self.ui:AddEventListener(Event.COMPLETE, self, self.onComplete)
end

---onComplete
---@param evt Framework.event.Event
function logic_03:onComplete(evt)
    self.ui.m_Text.text = evt.name
end

return logic_03