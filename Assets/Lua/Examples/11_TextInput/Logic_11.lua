require("Examples.11_TextInput.UI_11")

---@class Logic_11
---@field ui Examples.11_TextInput.UI_11
Logic_11 = class("Logic_11")

function Logic_11:ctor()
    self.ui = UI_11.New()
    --self.ui.InputField:SetInteractable(false)
    self.ui.InputField.EventEndEdit:Add(self, self.onEndEdit)
end

---onEndEdit
---@param evt Framework.event.Event
function Logic_11:onEndEdit(evt)
    self.ui.Text:SetText(self.ui.InputField:GetText())
end

return Logic_11
