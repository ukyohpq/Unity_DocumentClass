require("CustomGame.UI.TestContainer")
local super = require("puremvc.patterns.mediator.Mediator")

---@class CustomGame.fu.testfu.TestMediator:Mediator
TestMediator = class("CustomGame.fu.testfu.TestMediator", super)

function TestMediator:ctor(viewComponent)
    super.ctor(self, self.__cname, viewComponent)
end

function TestMediator:show()
    if self.viewComponent == nil then
        local cls = self:getUIClass()
        local ui = cls.New()
        self:setViewComponent(ui)
        ui:AddEventListener("Complete", self, self.onUILoaded)
        ui:LoadResource()
    else
        self:showUI()
    end
end

---onUILoaded
---@param evt Framework.event.Event
function TestMediator:onUILoaded(evt)
    self:showUI()
end

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