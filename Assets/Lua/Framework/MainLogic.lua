---@class MainLogic
MainLogic = class("Framework.MainLogic")

function MainLogic:ctor()
    
end

function MainLogic:Update(deltaTime, unscaledDeltaTime)
    LogUtil.LogError("MainLogic:Update")
end

function MainLogic:FixedUpdate(fixedDeltaTime, fixedUnscaledTime)
    LogUtil.LogError("MainLogic:FixedUpdate")
end