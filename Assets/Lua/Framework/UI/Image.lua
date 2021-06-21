local super = require("Framework.display.DisplayObject")

---@class Framework.UI.Image:Framework.display.DisplayObject
Image = class("Framework.UI.Image", super)

function Image:SetImage(...)
    self:SetTextExtend(...)
end

return Image