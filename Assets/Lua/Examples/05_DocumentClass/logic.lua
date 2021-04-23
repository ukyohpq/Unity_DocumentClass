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
end

---onComplete
---@param evt Framework.event.Event
function logic_05:onComplete(evt)
    self.ui.m_Doc.m_Button:AddEventListener("click", self, self.onClick)
end

---onClick
---@param evt Framework.event.Event
function logic_05:onClick(evt)
    self.numClick = self.numClick + 1
    self.ui.m_Doc.m_Text.text = "total click" .. self.numClick
    LogUtil.LogError("target:%s", evt.target.__cname)
    --local ui = self.ui
    --self.ui = nil
    --ui:Destroy()
end

return logic_05