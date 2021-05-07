local super = require("Framework.UI.CLBinder")

---@class Framework.UI.Image:Framework.UI.CLBinder
Image = class("Framework.UI.Image", super)

function Image:SetImage()
    LogUtil.LogError("SetImage")
end

return Image