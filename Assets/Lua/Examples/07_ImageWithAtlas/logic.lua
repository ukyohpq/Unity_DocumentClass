require("Examples.07_ImageWithAtlas.UI_07")
require("Framework.event.Event")

---@class Examples.07_ImageWithAtlas.logic_07
---@field ui Examples.07_ImageWithAtlas.UI_07
---@field imgSwitch boolean
logic_07 = class("Examples.07_ImageWithAtlas.logic_07")

function logic_07:ctor()
    self.ui = UI_07.New()
    self.imgSwitch = true
    self.ui.m_Button.EventClick:Add(self, self.onClick)
    self.ui.m_Image.EventComplete:Add(self, self.onSpriteComplete)
end

---onClick
---@param evt Framework.event.Event
function logic_07:onClick(evt)
    self.imgSwitch = not self.imgSwitch
    if self.imgSwitch then
        self.ui.m_Image:SetAtlasImage("Assets/UI/Atlas/Example/Arena.png", "text_ping")
    else
        self.ui.m_Image:SetAtlasImage("Assets/UI/Atlas/Example/Arena.png", "text_sheng")
    end
end

---onSpriteComplete
---@param evt Framework.event.Event
function logic_07:onSpriteComplete(evt)
    LogUtil.LogError("onComplete:%s", evt:GetEventName())
end

return logic_07