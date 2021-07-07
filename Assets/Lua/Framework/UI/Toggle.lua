local super = DisplayObject

---@class Framework.UI.Toggle:Framework.display.DisplayObject
---@field isOn boolean
---@field EventToggleChanged Framework.event.Delegate
Toggle = class("Framework.UI.Toggle", DisplayObject)

function Toggle:ctor()
    super.ctor(self)
    self.isOn = false
    self.EventToggleChanged = Delegate.New("ToggleChanged", self)
end

function Toggle:onBind()
    self:SetIsOnExtend(self.isOn)
end

function Toggle:GetIsOn()
    return self.isOn
end

function Toggle:SetIsOn(isOn)
    if self.isOn == isOn then
        return
    end
    self.isOn = isOn
    self.SetIsOnExtend(isOn)
end

return Toggle