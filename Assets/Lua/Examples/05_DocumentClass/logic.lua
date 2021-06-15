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
    local doc = UI_Doc.New()
    self.ui:AddChild(doc)
    self.ui:AddEventListener("click", self, self.onClick)
end

---onClick
---@param evt Framework.event.Event
function logic_05:onClick(evt)
    evt:StopBubble()
    LogUtil.LogError("curtarget:%s target:%s", evt:GetCurrentTarget().name, evt:GetTarget().name)
    ---@type Framework.UI.Button
    local btn = evt:GetTarget()
    btn:GetParent():Destroy()
end

return logic_05