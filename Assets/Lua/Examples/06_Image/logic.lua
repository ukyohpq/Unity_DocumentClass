require("Examples.06_Image.UI_06")
require("Framework.event.Event")

---@class Examples.06_Image.logic_06
---@field ui Examples.06_Image.UI_06
---@field imgSwitch boolean
logic_06 = class("Examples.05_DocumentClass.logic_06")

function logic_06:ctor()
    self.ui = UI_06.New()
    self.imgSwitch = true
    self.ui.m_Button.EventClick:Add(self, self.onClick)
    self.ui.m_Image.EventComplete:Add(self, self.onSpriteComplete)
end

---onClick
---@param evt Framework.event.Event
function logic_06:onClick(evt)
    self.imgSwitch = not self.imgSwitch
    if self.imgSwitch then
        self.ui.m_Image:SetImage("Assets/Lua/Examples/06_Image/ButtonAcceleratorOverSprite.png")
    else
        self.ui.m_Image:SetImage("Assets/Lua/Examples/06_Image/img_fenghuang_03.png")
    end
end

---onSpriteComplete
---@param evt Framework.event.Event
function logic_06:onSpriteComplete(evt)
    LogUtil.LogError("onComplete:%s", evt:GetEventName())
end

return logic_06