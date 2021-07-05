local super = require("Framework.display.DisplayObject")

---@class Framework.UI.Button:Framework.display.DisplayObject
---@field OnClick Framework.event.Delegate
---@field OnDown Framework.event.Delegate
---@field OnUp Framework.event.Delegate
Button = class("Framework.UI.Button", super)

function Button:ctor()
    super.ctor(self)
    self.OnClick = Delegate.New("click", self)
    self.OnDown = Delegate.New("down", self)
    self.OnUp = Delegate.New("up", self)
end

return Button