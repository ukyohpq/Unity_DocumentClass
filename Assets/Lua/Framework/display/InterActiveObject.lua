local super = require("Framework.display.DisplayObject")
---@class Framework.display.InterActiveObject:Framework.display.DisplayObject
---@field EventClick Framework.event.Delegate
---@field EventDown Framework.event.Delegate
---@field EventUp Framework.event.Delegate
---@field EventEnter Framework.event.Delegate
---@field EventExit Framework.event.Delegate
---@field EventDrop Framework.event.Delegate
InterActiveObject = class("Framework.display.InterActiveObject", super)

function InterActiveObject:ctor()
    super.ctor(self)
    self.EventClick = Delegate.New("click", self)
    self.EventDown = Delegate.New("down", self)
    self.EventUp = Delegate.New("up", self)
    self.EventEnter = Delegate.New("enter", self)
    self.EventExit = Delegate.New("exit", self)
    self.EventDrop = Delegate.New("drop", self)
end

return InterActiveObject