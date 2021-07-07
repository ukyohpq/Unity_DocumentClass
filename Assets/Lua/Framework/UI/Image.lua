local super = require("Framework.display.DisplayObject")

---@class Framework.UI.Image:Framework.display.DisplayObject
---@field imageStr string
---@field EventComplete Framework.event.Delegate
Image = class("Framework.UI.Image", super)

function Image:ctor()
    super.ctor(self)
    self.imageStr = ""
    self.EventComplete = Delegate.New(Event.COMPLETE, self)
end

function Image:SetImage(imagePath)
    if self.imageStr == imagePath then
        return
    end
    self.imageStr = imagePath
    self:SetImageExtend(imagePath)
end

function Image:SetAtlasImage(atlasPath, imageName)
    local str = atlasPath .. "|" .. imageName
    if self.imageStr == str then
        return
    end
    self.imageStr = str
    self:SetImageExtend(atlasPath, imageName)
end

return Image