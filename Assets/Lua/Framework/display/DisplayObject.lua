local super = require("Framework.event.EventDispatcher")

---@class Framework.display.DisplayObject:Framework.event.EventDispatcher
---@field private isDestroyed boolean
---@field private name string
---@field private parent Framework.display.DisplayObjectContainer
---@field private active boolean
---@field private dragging boolean
---@field EventBind Framework.event.Delegate
DisplayObject = class("Framework.display.DisplayObject", super)

function DisplayObject:ctor()
    super.ctor(self)
    self.isDestroyed = false
    self.draggable = false
    self.active = true
    self.EventBind = Delegate.New(Event.ON_BIND, self)
    self.EventBind:Add(self, self.onBind)
end

function DisplayObject:Destroy()
    self.EventBind:Clear()
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

function DisplayObject:SetActive(active)
    if self.active == active then
        return
    end
    self.active = active
    self:SetActiveExtend(active)
end

---onInit
---@param evt Framework.event.Event
function DisplayObject:onBind(evt)
    self:SetActiveExtend(self.active)  
end

function DisplayObject:StartDrag()
    if self.dragging then
        LogUtil.LogWarning("is dragging already.")
        return
    end
    self.dragging = true
    self:StartDragExtend()
end

function DisplayObject:StopDrag()
    if not self.dragging then
        return
    end
    self.dragging = false
    self:StopDragExtend()
end

function DisplayObject:BackToDragStartPoint()
    self:BackToStartPointExtend()
end

return DisplayObject