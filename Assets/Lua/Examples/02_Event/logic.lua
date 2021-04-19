require("Examples.02_Event.UI_02")
require("Framework.event.Event")

---@class Examples.02_Event.logic
---@field ui Examples.02_Event.UI_02
logic = class("Examples.02_Event.logic")

function logic:ctor()
    self.ui = UI_02.New()
    self.ui:AddEventListener(Event.COMPLETE, self, self.onComplete)
end

---onComplete
---@param evt Framework.event.Event
function logic:onComplete(evt)
    LogUtil.LogError("evt:%s", evt.name)
end

return logic