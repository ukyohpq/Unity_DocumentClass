local super = require("Framework.event.EventDispatcher")

---@class Framework.UI.CLBinder:Framework.event.EventDispatcher
---@field private ___GameObjectLuaBinder___ Framework.core.GameObjectLuaBinder
CLBinder = class("Framework.UI.CLBinder", super)

function CLBinder:Destroy()
    LogUtil.LogError("try CSDestroy:%s", tostring(self))
    if self.___GameObjectLuaBinder___ then
        LogUtil.LogError("CSDestroy:%s", tostring(self))
        self.___GameObjectLuaBinder___:CSDestroy()
    end
end

function CLBinder:getName()
    if self.___GameObjectLuaBinder___ then
        return self.___GameObjectLuaBinder___.name
    else
        return nil
    end
end

return CLBinder