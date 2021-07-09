local super = DisplayObject

---@class Framework.UI.ScrollViewPanel:Framework.display.DisplayObject
ScrollViewPanel = class("Framework.UI.ScrollViewPanel", DisplayObject)

function ScrollViewPanel:ctor()
    super.ctor(self)
end

return ScrollViewPanel