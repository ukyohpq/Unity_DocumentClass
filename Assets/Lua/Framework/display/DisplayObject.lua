local super = require("Framework.event.EventDispatcher")

---@class Framework.display.DisplayObject:Framework.event.EventDispatcher
---@field parent Framework.display.DisplayObjectContainer
---@field name string
DisplayObject = class("Framework.display.DisplayObject", super)

function DisplayObject:GetParent()
    return self.parent
end

function DisplayObject:SetName(name)
    self.name = name
end

function DisplayObject:GetName()
    return self.name
end

return DisplayObject
