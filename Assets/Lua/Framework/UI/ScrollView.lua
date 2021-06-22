local super = DisplayObjectContainer

---@class Framework.UI.ScrollView:Framework.display.DisplayObjectContainer
ScrollView = class("Framework.UI.ScrollView", DisplayObjectContainer)

function ScrollView:ctor()
    super.ctor(self)
end

return ScrollView