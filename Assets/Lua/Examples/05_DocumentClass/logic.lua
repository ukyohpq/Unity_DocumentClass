require("Examples.05_DocumentClass.UI_05")
require("Framework.event.Event")

---@class Examples.05_DocumentClass.logic_05
---@field ui Examples.05_DocumentClass.UI_05
---@field numClick number
logic_05 = class("Examples.05_DocumentClass.logic_05")

function logic_05:ctor()
    self.numClick = 0
    self.ui = UI_05.New()
    self.ui:AddEventListener(Event.COMPLETE, self, self.onComplete)
    --self.ui:Destroy()
    --self.ui = null
end

---onComplete
---@param evt Framework.event.Event
function logic_05:onComplete(evt)
    self.ui.m1_Doc.m_Button:AddEventListener("click", self, self.onClick)
    self.ui.m2_Doc.m_Button:AddEventListener("click", self, self.onClick)
end

---onClick
---@param evt Framework.event.Event
function logic_05:onClick(evt)
    ---@type Framework.UI.Button
    local btn = evt.target
    if btn == self.ui.m1_Doc.m_Button then
        LogUtil.LogError("click 1")
    elseif btn == self.ui.m2_Doc.m_Button then
        LogUtil.LogError("click 2")
    end
end

return logic_05