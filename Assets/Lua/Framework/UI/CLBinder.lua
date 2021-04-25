local super = require("Framework.event.EventDispatcher")

---@class Framework.UI.CLBinder:Framework.event.EventDispatcher
CLBinder = class("Framework.UI.CLBinder", super)

function CLBinder:Destroy()
    LogUtil.LogError("try CSDestroy:%s", tostring(self))
    if self.DestroyToCS then
        self:DestroyToCS()
    end
end

function CLBinder:getName()
    return self.name
end

return CLBinder