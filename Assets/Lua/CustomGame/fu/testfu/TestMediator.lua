require("CustomGame.UI.TestContainer")
local super = GameMediator

---@class CustomGame.fu.testfu.TestMediator:Framework.core.mvc.GameMediator
TestMediator = class("CustomGame.fu.testfu.TestMediator", super)



function TestMediator:showUI()
    ---@type CustomGame.UI.TestContainer
    local prefab = self.viewComponent
    prefab.m_Doc.m_Text.text = "这是文本1"
    prefab.m_Doc.m2_Text.text = "这是文本2"
    prefab.m_Doc.m_Button:AddEventListener("click", self, self.onClick)
end

function TestMediator:onClick()
    ---@type CustomGame.UI.TestContainer
    local prefab = self.viewComponent
    prefab.m_Doc.m2_Text.text = "按钮被点击了"
end

function TestMediator:getUIClass()
    return TestContainer
end

function TestMediator:handleNotification(notification)

end

function TestMediator:listNotificationInterests()
    return {}
end

return TestMediator