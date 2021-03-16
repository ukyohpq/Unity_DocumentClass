---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class CustomGame.UI.TestUI:Framework.UI.Prefab
---@field m_Text UnityEngine.UI.Text
---@field m_Button Framework.UI.Button
TestUI = class("CustomGame.UI.TestUI", super)

function TestUI:OnComplete(evt)
    LogUtil.LogError("OnComplete target:%s", evt.target == self)
    self.m_Button:AddEventListener("click", self, self.onClick)
    self.m_Text.text = "hello tolua"
end

function TestUI:GetAssetPath()
    if IsEditor then
        return "Assets/prefabs/TestUI.prefab"
    else
        return "2"
    end
end

function TestUI:onClick(...)
    LogUtil.LogError("onClick")
end

return TestUI
