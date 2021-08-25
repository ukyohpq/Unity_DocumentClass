local super = require("Framework.event.EventDispatcher")

--ui显示对象的生命监控
local instanceMap = {}
--ui显示对象数量
local numInstance = 0

---@class Framework.display.DisplayObject:Framework.event.EventDispatcher
---@field private isDestroyed boolean
---@field private name string
---@field private parent Framework.display.DisplayObjectContainer
---@field private active boolean
---@field private dragging boolean
---@field private parentTransform boolean
---@field EventBind Framework.event.Delegate
---@field state number
DisplayObject = class("Framework.display.DisplayObject", super)

function DisplayObject:ctor()
    super.ctor(self)
    self.isDestroyed = false
    self.draggable = false
    self.active = true
    self.EventBind = Delegate.New(Event.ON_BIND, self)
    self.EventBind:Add(self, self.onBind)
    instanceMap[self] = true
    numInstance = numInstance + 1
    self.parentTransform = false
    self:SetState(0)
end

function DisplayObject:Destroy()
    self.EventBind:Clear()
    self:RemoveAllEventListeners()
    --LogUtil.LogError("try CSDestroy:%s", self:GetName())
    self.isDestroyed = true
    if self.DestroyToCS then
        self:DestroyToCS()
    end
    self.parent = nil
    self.parentTransform = false
    instanceMap[self] = nil
    numInstance = numInstance - 1
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

---StopDrag
---@return Framework.display.InterActiveObject
function DisplayObject:StopDrag()
    if not self.dragging then
        return nil
    end
    self.dragging = false
    return self:StopDragExtend()
end

function DisplayObject:BackToDragStartPoint()
    self:BackToStartPointExtend()
end

function DisplayObject:SetState(state)
    if not self.state == state then
        self.state = state
    end
end

return DisplayObject