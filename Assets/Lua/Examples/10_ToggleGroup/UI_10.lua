----------------------------- 以下为 UI代码 可以修改 -----------------------------------
---@type Examples.common.CommonPrefab
local super = require("Examples.common.CommonPrefab")

---@class Examples.10_ToggleGroup.UI_10:Examples.common.CommonPrefab
---@field Image Framework.UI.Image
---@field Button Framework.UI.Button
UI_10 = class("Examples.10_ToggleGroup.UI_10", super)

function UI_10:ctor(autoBind)
    super.ctor(self, autoBind)
	self.Image = Image.New()
	self:AddChild(self.Image)
	self.Button = Button.New()
	self:AddChild(self.Button)
end

function UI_10:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/10_ToggleGroup/UI_10.prefab"
    else
        return "2"
    end
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
---@field Framework.event.Event
function UI_10:OnInit(evt)
    super.OnInit(self, evt)
    LogUtil.LogError("dfgf")
end

return UI_10
