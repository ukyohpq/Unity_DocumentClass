local super = DisplayObjectContainer
---@class ToggleGroup:Framework.display.DisplayObjectContainer
---@field activeIndex number
ToggleGroup = class("ToggleGroup", DisplayObjectContainer)

function ToggleGroup:ctor()
    
end

function ToggleGroup:AddChildAt(child, index)
    if not child:is(Toggle) then
        error(string.format("ToggleGroup must add Toggle, not:%s", child.__cname))
    end
    super.AddChildAt(self, child, index)
    child:AddEventListener("ToggleChange", self, self.onToggleChange)
end

---onToggleChange
---@param evt Framework.event.Event
function ToggleGroup:onToggleChange(evt)
    
end

function ToggleGroup:GetActiveIndex()
    return self.activeIndex
end

return ToggleGroup