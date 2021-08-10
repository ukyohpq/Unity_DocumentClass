local super = require("Framework.display.DisplayObject")
---@class Framework.display.InterActiveObject:Framework.display.DisplayObject
---@field EventClick Framework.event.Delegate
---@field EventDown Framework.event.Delegate
---@field EventUp Framework.event.Delegate
---@field EventDragEnter Framework.event.Delegate
---@field EventDragExit Framework.event.Delegate
InterActiveObject = class("Framework.display.InterActiveObject", super)

function InterActiveObject:ctor()
    super.ctor(self)
    self.EventClick = Delegate.New("click", self)
    self.EventDown = Delegate.New("down", self)
    self.EventUp = Delegate.New("up", self)
    self.EventDragEnter = Delegate.New("dragEnter", self)
    self.EventDragExit = Delegate.New("dragExit", self)
end

return InterActiveObject