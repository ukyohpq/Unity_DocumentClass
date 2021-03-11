require("CustomGame.UI.TestUI")
---@class MainLogic
MainLogic = class("Framework.MainLogic")

function MainLogic:ctor()
    local testUI = TestUI.New()
    testUI:LoadResource()
end

function MainLogic:Update(deltaTime, unscaledDeltaTime)
    --LogUtil.LogError("MainLogic:Update")
end

function MainLogic:FixedUpdate(fixedDeltaTime, fixedUnscaledTime)
    --LogUtil.LogError("MainLogic:FixedUpdate")
end

function MainLogic:CSCallLua()

end