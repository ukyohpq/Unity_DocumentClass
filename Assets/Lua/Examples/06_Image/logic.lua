require("Examples.06_Image.UI_06")
require("Framework.event.Event")

---@class Examples.06_Image.logic_06
---@field ui Examples.06_Image.UI_06
logic_06 = class("Examples.05_DocumentClass.logic_06")

function logic_06:ctor()
    self.ui = UI_06.New()
    self.ui:AddEventListener(Event.COMPLETE, self, self.onComplete)
end

---onComplete
---@param evt Framework.event.Event
function logic_06:onComplete(evt)
    --self.ui.m_Image.sprite
end

return logic_06