require("CustomGame.UI.TestContainer")
local super = GameMediator

---@class CustomGame.fu.testfu.TestMediator:Framework.core.mvc.GameMediator
---@field numClick number
TestMediator = class("CustomGame.fu.testfu.TestMediator", super)

function TestMediator:showUI()
    ---@type CustomGame.UI.TestContainer
    local prefab = self.viewComponent
    prefab.m_Doc.m_Text.text = "这是文本1"
    prefab.m_Doc.m2_Text.text = "这是文本2"
    prefab.m_Doc.m_Button:AddEventListener("click", self, self.onClick)
    self.numClick = 0
end

function TestMediator:onClick()
    ---@type CustomGame.UI.TestContainer
    local prefab = self.viewComponent
    self.numClick = self.numClick + 1
    prefab.m_Doc.m2_Text.text = "按钮被点击了" .. self.numClick .. "次"
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