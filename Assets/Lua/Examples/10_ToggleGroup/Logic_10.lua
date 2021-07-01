require("Examples.10_ToggleGroup.UI_10")

---@class Logic_10
---@field ui Examples.10_ToggleGroup.UI_10
Logic_10 = class("Logic_10")

function Logic_10:ctor()
    self.ui = UI_10.New()
    self.ui.ToggleGroup:AddEventListener("ToggleGroupChanged", self, self.onToggleGroupChanged)
    self.ui.Toggle1:AddEventListener("ToggleChanged", self, self.onToggleChanged)
    self.ui.Toggle2:AddEventListener("ToggleChanged", self, self.onToggleChanged)
    self.ui.Text:SetText("还没点呢")
end

---onClick
---@param evt Framework.event.Event
function Logic_10:onToggleGroupChanged(evt)
    self.ui.Text:SetText(evt:GetEventData()[1].name)
end

---onToggleChanged
---@param evt Framework.event.Event
function Logic_10:onToggleChanged(evt)
    LogUtil.LogError("onToggleChanged:%s", evt:GetTarget().name)
end

return Logic_10
