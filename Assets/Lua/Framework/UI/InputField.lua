local super = DisplayObject
---@class Framework.UI.InputField:Framework.display.DisplayObject
---@field interactable boolean
---@field EventEndEdit Framework.event.Delegate
---@field text string
InputField = class("Framework.UI.InputField", super)

function InputField:ctor()
    super.ctor(self)
    self.interactable = true
    self.EventEndEdit = Delegate.New("OnEndEdit", self)
    self.EventEndEdit:Add(self, self.onEndEdit)
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

---onEndEdit
---@param evt Framework.event.Event
function InputField:onEndEdit(evt)
    self.text = evt:GetEventData()[1] or ""
end

function InputField:GetText()
    return self.text
end

return InputField