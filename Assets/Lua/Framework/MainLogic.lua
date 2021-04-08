require("CustomGame.GameFacade")

---@class MainLogic
MainLogic = class("Framework.MainLogic")

function MainLogic:ctor()
    local facade = GameFacade.New()
    facade:Start()
end

function MainLogic:Update(deltaTime, unscaledDeltaTime)
    --LogUtil.LogError("MainLogic:Update")
end

function MainLogic:FixedUpdate(fixedDeltaTime, fixedUnscaledTime)
    --LogUtil.LogError("MainLogic:FixedUpdate")
end

return MainLogic