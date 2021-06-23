local super = require("Framework.event.EventDispatcher")

---@class Framework.display.DisplayObject:Framework.event.EventDispatcher
---@field private isDestroyed boolean
---@field private name string
---@field private parent Framework.display.DisplayObjectContainer
---@field private draggable boolean
DisplayObject = class("Framework.display.DisplayObject", super)

function DisplayObject:ctor()
    super.ctor(self)
    self.isDestroyed = false
    self.draggable = false
end

function DisplayObject:Destroy()
    self:RemoveAllEventListeners()
    --LogUtil.LogError("try CSDestroy:%s", self:GetName())
    self.isDestroyed = true
    if self.DestroyToCS then
        self:DestroyToCS()
    end
end

function DisplayObject:GetParent()
    return self.parent
end

function DisplayObject:GetName()
    --注意一定不要给name赋值，这是个只读字段
    return self.name
end

function DisplayObject:getIsDestroyed()
    return self.isDestroyed
end

function DisplayObject:DispatchEvent(evt)
    super.DispatchEvent(self, evt)
    if evt:IsBubble() and self.parent then
        self.parent:DispatchEvent(evt)
    end
end

function DisplayObject:SetDraggable(draggable)
    self.draggable = draggable
end

return DisplayObject