require("Examples.06_Image.UI_06")
require("Framework.event.Event")

---@class Examples.06_Image.logic
---@field ui Examples.06_Image.UI_06
logic = class("Examples.05_DocumentClass.logic")

function logic:ctor()
    self.ui = UI_06.New()
    self.ui:LoadResource()
    self.ui:AddEventListener(Event.COMPLETE, self, self.onComplete)
end

---onComplete
---@param evt Framework.event.Event
function logic:onComplete(evt)
    --self.ui.m_Image.sprite
end

return logic