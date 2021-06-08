local super = require("Framework.event.EventDispatcher")

---@class Framework.display.DisplayObject:Framework.event.EventDispatcher
---@field private isDestroyed boolean
---@field private name string
---@field private parent Framework.display.DisplayObjectContainer
DisplayObject = class("Framework.display.DisplayObject", super)

function DisplayObject:ctor()
    super.ctor(self)
    self:bind()
    self.isDestroyed = false
end

function DisplayObject:Destroy()
    self:RemoveAllEventListeners()
    LogUtil.LogError("try CSDestroy:%s", self:GetName())
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

---bind 只能被调用一次
function DisplayObject:bind()
    self.bind = self.afterBind
end

function DisplayObject:afterBind()
    error("function bind can not be called only once")
end

function DisplayObject:getIsDestroyed()
    return self.isDestroyed
end

function DisplayObject:DispatchEvent(evt)
    if evt:GetTarget() == nil then
        evt:UseBubble()
    end
    super.DispatchEvent(self, evt)
    if evt:IsBubble() and self.parent then
        self.parent:DispatchEvent(evt)
    end
end

return DisplayObject