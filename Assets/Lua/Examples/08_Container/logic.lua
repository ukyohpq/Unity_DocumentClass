require("Examples.08_Container.Container")
require("Examples.08_Container.Child")
require("Framework.event.Event")

---@class Examples.08_Container.logic_08
---@field ui Examples.08_Container.Container
logic_08 = class("Examples.08_Container.logic_08")

function logic_08:ctor()
    self.ui = Container.New()
    self.ui:AddEventListener(Event.COMPLETE, self, self.onComplete)
end

---onComplete
---@param evt Framework.event.Event
function logic_08:onComplete(evt)
    local child = Child.New()
    self.ui:AddChild(child)
    child:AddEventListener(Event.COMPLETE, self, self.childComplete)
    self.ui:AddEventListener("click", self, self.onClickUI)
    child:AddEventListener("click", self, self.onClickChild)
end

function logic_08:childComplete(evt)
    local child = self.ui:GetChildAt(1)
    child.m_Button:AddEventListener("click", self, self.onClickBtn)
end

---onClickUI
---@param evt Framework.event.Event
function logic_08:onClickUI(evt)
    LogUtil.LogError("onClickUI")
end

---onClickChild
---@param evt Framework.event.Event
function logic_08:onClickChild(evt)
    LogUtil.LogError("onClickChild")
    evt:StopBubble()
end

---onClickBtn
---@param evt Framework.event.Event
function logic_08:onClickBtn(evt)
    LogUtil.LogError("onClickBtn")
end

return logic_08
