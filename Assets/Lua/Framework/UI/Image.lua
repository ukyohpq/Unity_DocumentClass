local super = require("Framework.Display.DisplayObject")

---@class Framework.UI.Image:Framework.Display.DisplayObject
Image = class("Framework.UI.Image", super)

function Image:SetImage()
    LogUtil.LogError("SetImage")
end

return Image