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

---onComplete
---@param evt Framework.event.Event
function MainLogic:onComplete(evt)
    LogUtil.LogError("onComplete target:%s", evt:GetCurrentTarget())
    ---@type CustomGame.UI.TestContainer
    local testUI = evt:GetCurrentTarget()
    testUI.m_Doc.m2_Text.text = "this is text2"
    testUI.m_Doc.m_Text.text = "this is text1"
    LogUtil.LogError("doc:%s", testUI.m_Doc)
    LogUtil.LogError("text1:%s", testUI.m_Doc.m_Text)
    LogUtil.LogError("text2:%s", testUI.m_Doc.m2_Text)
    testUI.m_Doc.m_Button:AddEventListener("click", self, self.onClick)
    --testUI.m_Text.text = "hello tolua"
    --testUI.m_Button:AddEventListener("click", self, self.onClick)
    --testUI.m_Button:AddEventListener("click", self, self.onClick2)
    --testUI.m_Button:RemoveEventListener("click", self, self.onClick)
end

---onClick
function MainLogic:onClick()
    LogUtil.LogError("MainLogic:onClick self:%s", self.__cname)
end

function MainLogic:onClick2(...)
    LogUtil.LogError("MainLogic:onClick self:%s", 2)
end


return MainLogic