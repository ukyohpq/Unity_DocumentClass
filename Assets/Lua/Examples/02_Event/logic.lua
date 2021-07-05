require("Examples.02_Event.UI_02")
require("Framework.event.Event")

---@class Examples.02_Event.logic_02
---@field ui Examples.02_Event.UI_02
logic_02 = class("Examples.02_Event.logic_02")

function logic_02:ctor()
    self.ui = UI_02.New()
    self.ui.OnInit:Add(self, self.onInit)
end

---onComplete
---@param evt Framework.event.Event
function logic_02:onInit(evt)
    LogUtil.LogError("evt:%s", evt.name)
end

return logic_02