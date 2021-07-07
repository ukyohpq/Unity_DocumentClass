local super = require("Framework.display.DisplayObject")

---@class Framework.UI.Button:Framework.display.DisplayObject
---@field EventClick Framework.event.Delegate
---@field EventDown Framework.event.Delegate
---@field EventUp Framework.event.Delegate
Button = class("Framework.UI.Button", super)

function Button:ctor()
    super.ctor(self)
    self.EventClick = Delegate.New("click", self)
    self.EventDown = Delegate.New("down", self)
    self.EventUp = Delegate.New("up", self)
end

return Button