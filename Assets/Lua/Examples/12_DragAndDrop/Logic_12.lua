require("Examples.12_DragAndDrop.UI_12")
---@class Logic_12
---@field ui Examples.12_DragAndDrop.UI_12
Logic_12 = class("Logic_12")

function Logic_12:ctor()
    self.ui = UI_12.New()
    self.ui.Image.EventDown:Add(self, self.onDown)
    self.ui.Image.EventUp:Add(self, self.onUp)
    self.ui.Image_1.EventDragEnter:Add(self, self.onEnter)
    self.ui.Image_1.EventDragExit:Add(self, self.onExit)
end

function Logic_12:onDown(evt)
    self.ui.Image:StartDrag()
end

function Logic_12:onUp(evt)
    local dropObj = self.ui.Image:StopDrag()
    if dropObj ~= nil then
        LogUtil.LogError("drop:%s", dropObj:GetName())
        self.ui.Image:BackToDragStartPoint()
    end
end

function Logic_12:onEnter(evt)
    LogUtil.LogError("enter")
end

function Logic_12:onExit(evt)
    LogUtil.LogError("exit")
end

return Logic_12
