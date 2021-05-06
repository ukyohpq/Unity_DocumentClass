local super = require("Framework.event.EventDispatcher")

---@class Framework.UI.CLBinder:Framework.event.EventDispatcher
---@field private isDestroyed boolean
CLBinder = class("Framework.UI.CLBinder", super)

function CLBinder:ctor()
    super.ctor(self)
    self:bind()
    self.isDestroyed = false
end

function CLBinder:Destroy()
    self:RemoveAllEventListeners()
    LogUtil.LogError("try CSDestroy:%s", self:getName())
    self.isDestroyed = true
    if self.DestroyToCS then
        self:DestroyToCS()
    end
end

function CLBinder:getName()
    --注意一定不要给name赋值，这是个只读字段
    return self.name
end

---bind 只能被调用一次
function CLBinder:bind()
    self.bind = self.afterBind
end

function CLBinder:afterBind()
    error("function bind can not be called only once")
end

function CLBinder:getIsDestroyed()
    return self.isDestroyed
end

return CLBinder