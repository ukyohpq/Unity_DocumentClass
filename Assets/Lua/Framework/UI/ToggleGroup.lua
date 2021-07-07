local super = DisplayObject
---@class Framework.UI.ToggleGroup:Framework.display.DisplayObject
---@field activeIndex number
---@field EventToggleGroupChanged Framework.event.Delegate
ToggleGroup = class("Framework.UI.ToggleGroup", super)

function ToggleGroup:ctor()
    super.ctor(self)
    self.EventToggleGroupChanged = Delegate.New("ToggleGroupChanged", self)
end

return ToggleGroup