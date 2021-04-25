local super = require("Framework.event.EventDispatcher")

---@class Framework.UI.CLBinder:Framework.event.EventDispatcher
CLBinder = class("Framework.UI.CLBinder", super)

function CLBinder:Destroy()
    LogUtil.LogError("try CSDestroy:%s", self:getName())
    if self.DestroyToCS then
        self:DestroyToCS()
    end
end

function CLBinder:getName()
    --注意一定不要给name赋值，这是个只读字段
    return self.name
end

return CLBinder