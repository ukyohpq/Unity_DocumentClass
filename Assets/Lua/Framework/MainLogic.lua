require("CustomGame.UI.TestUI")
---@class MainLogic
MainLogic = class("Framework.MainLogic")

function MainLogic:ctor()
    local testUI = TestUI.New()
    testUI:AddEventListener("Complete", self, self.onComplete)
    testUI:LoadResource()
end

function MainLogic:Update(deltaTime, unscaledDeltaTime)
    --LogUtil.LogError("MainLogic:Update")
end

function MainLogic:FixedUpdate(fixedDeltaTime, fixedUnscaledTime)
    --LogUtil.LogError("MainLogic:FixedUpdate")
end

---onComplete
---@param evt Framework.event.Event
function MainLogic:onComplete(evt)
    LogUtil.LogError("onComplete target:%s", evt:GetCurrentTarget())
    ---@type CustomGame.UI.TestUI
    local testUI = evt:GetCurrentTarget()
    LogUtil.LogError("testUI:%s", testUI.m_Text.text)
    testUI.m_Text.text = "hello tolua"
    testUI.m_Button:AddEventListener("click", self, self.onClick)
end

function MainLogic:onClick(...)
    LogUtil.LogError("MainLogic:onClick self:%s", 1)
end

return MainLogic