local super = require("Framework.event.EventDispatcher")

---@class Framework.Display.DisplayObject:Framework.event.EventDispatcher
---@field private isDestroyed boolean
---@field private name string
---@field parent Framework.display.DisplayObjectContainer
DisplayObject = class("Framework.UI.DisplayObject", super)

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

return DisplayObject