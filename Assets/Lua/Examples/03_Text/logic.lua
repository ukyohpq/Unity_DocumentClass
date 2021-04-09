require("Examples.03_Text.UI_03")
require("Framework.event.Event")

---@class Examples.03_Text.logic
---@field ui Examples.03_Text.UI_03
logic = class("Examples.03_Text.logic")

function logic:ctor()
    self.ui = UI_03.New()
    self.ui:LoadResource()
    self.ui:AddEventListener(Event.COMPLETE, self, self.onComplete)
end

---onComplete
---@param evt Framework.event.Event
function logic:onComplete(evt)
    self.ui.m_Text.text = evt.name
end

return logic