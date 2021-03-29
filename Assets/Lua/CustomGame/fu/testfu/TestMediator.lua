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
    
    self.mp = {}
    setmetatable(self.mp, {
        __mode = "k",
    })
    local ui = TestUI.New()
    self.mp[ui] = true
    
    local xxx = {}
    local mt = {
        __gc = function(tb)
            LogUtil.LogError("gc")
        end
    }
    setmetatable(xxx, mt)
    self.mp[xxx] = true
end

function TestMediator:onClick()
    ---@type CustomGame.UI.TestContainer
    local prefab = self.viewComponent
    prefab.m_Doc.m2_Text.text = "按钮被点击了"

    for k, v in pairs(self.mp) do
        LogUtil.LogError("k:%s, v:%s", k, v)
    end
    collectgarbage()
    for k, v in pairs(self.mp) do
        LogUtil.LogError("k:%s, v:%s", k, v)
    end
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