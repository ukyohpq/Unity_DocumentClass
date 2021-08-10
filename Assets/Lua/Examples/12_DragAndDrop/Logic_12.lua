require("Examples.12_DragAndDrop.UI_12")
---@class Logic_12
---@field ui Examples.12_DragAndDrop.UI_12
Logic_12 = class("Logic_12")

function Logic_12:ctor()
    self.ui = UI_12.New()
    self.ui.Image.EventDown:Add(self, self.onDown)
    self.ui.Image.EventUp:Add(self, self.onUp)
    self.ui.Image_1.EventEnter:Add(self, self.onEnter)
    self.ui.Image_1.EventExit:Add(self, self.onExit)
    self.ui.Image_1.EventDrop:Add(self, self.onDrop)
end

function Logic_12:onDown(evt)
    self.ui.Image:StartDrag()
end

function Logic_12:onUp(evt)
    if self.ui.Image:StopDrag() then
        self.ui.Image:BackToDragStartPoint()
    end
end

function Logic_12:onEnter(evt)
    LogUtil.LogError("enter")
end

function Logic_12:onExit(evt)
    LogUtil.LogError("exit")
end

function Logic_12:onDrop(evt)
    LogUtil.LogError("drop")
end

return Logic_12
