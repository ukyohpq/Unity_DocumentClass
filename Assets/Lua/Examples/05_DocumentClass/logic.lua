require("Examples.05_DocumentClass.UI_05")
require("Framework.event.Event")

---@class Examples.05_DocumentClass.logic_05
---@field ui Examples.05_DocumentClass.UI_05
---@field numClick number
logic_05 = class("Examples.05_DocumentClass.logic_05")

function logic_05:ctor()
    self.numClick = 0
    self.ui = UI_05.New()
    self.ui.m1_Doc.m_Text:SetText("按钮1")
    self.ui.m1_Doc.m_Button.EventClick:Add(self, self.onClick)
    self.ui.m2_Doc.m_Text:SetText("按钮2")
    self.ui.m2_Doc.m_Button.EventClick:Add(self, self.onClick)
    --self.ui:Destroy()
    --self.ui = null
    self.ui.EventLongDown:Add(self, self.onLong)
    self.ui.EventDown:Add(self, self.onDown)
    self.ui.EventUp:Add(self, self.onUp)
end

---onClick
---@param evt Framework.event.Event
function logic_05:onClick(evt)
    LogUtil.LogError("curtarget:%s target:%s", evt:GetCurrentTarget().name, evt:GetTarget().name)
    ---@type Framework.UI.Button
    local btn = evt:GetTarget()
    btn:GetParent():Destroy()
end

---onLong
---@param evt Framework.event.Event
function logic_05:onLong(evt)
    LogUtil.LogError("longDown:%s", evt.target.parent.name)
end

---onDown
---@param evt Framework.event.Event
function logic_05:onDown(evt)
    LogUtil.LogError("down:%s", evt.target.parent.name)
end

---onUp
---@param evt Framework.event.Event
function logic_05:onUp(evt)
    LogUtil.LogError("up:%s", evt.target.parent.name)
end
return logic_05