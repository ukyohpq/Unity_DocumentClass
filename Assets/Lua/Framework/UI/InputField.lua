local super = DisplayObject
---@class Framework.UI.InputField:Framework.display.DisplayObject
---@field interactable boolean
InputField = class("Framework.UI.InputField", super)

function InputField:ctor()
    super.ctor(self)
    self.interactable = true
end

function InputField:onBind(evt)
    super.onBind(self, evt)
    self:SetInteractableExtend(self.interactable)
end

---SetInteractable
---@param interactable boolean
function InputField:SetInteractable(interactable)
    if self.interactable ~= interactable then
        self.interactable = interactable
        self:SetInteractableExtend(self.interactable)
    end
end

function InputField:GetInteractable()
    return self.interactable
end

return InputField