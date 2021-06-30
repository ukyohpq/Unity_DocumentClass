require("Examples.10_ToggleGroup.UI_10")

---@class Logic_10
---@field ui Examples.10_ToggleGroup.UI_10
Logic_10 = class("Logic_10")

function Logic_10:ctor()
    self.ui = UI_10.New()
    self.ui.Button:AddEventListener("click", self, self.onClick)
end

---onClick
---@param evt Framework.event.Event
function Logic_10:onClick(evt)
    LogUtil.LogError("22222222")
end

return Logic_10
