
require("System.class")

---@class Framework.UI.ScrollView:Framework.display.DisplayObjectContainer
ScrollView = class("Framework.UI.ScrollView")

function ScrollView:ctor()
    super.ctor(self)
end

return ScrollView