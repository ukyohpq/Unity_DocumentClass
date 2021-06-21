local super = DisplayObject

---@class Framework.UI.TextField:Framework.display.DisplayObject
---@field text string
TextField = class("Framework.UI.TextField", super)

function TextField:ctor()
    super.ctor(self)
    self.text = ""
end

function TextField:SetText(text)
    if self.text == text then
        return
    end
    self.text = text
    self:SetTextExtend(text)
end

function TextField:GetText()
    return self.text
end

return TextField