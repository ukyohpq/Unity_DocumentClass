local super = require("Framework.display.DisplayObject")

---@class Framework.UI.Image:Framework.display.DisplayObject
Image = class("Framework.UI.Image", super)

function Image:SetImage()
    LogUtil.LogError("SetImage")
end

return Image