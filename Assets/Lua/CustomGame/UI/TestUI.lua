local super = require("Framework.UI.Prefab")

---@class CustomGame.UI.TestUI:Framework.UI.Prefab
---@field m_Text UnityEngine.UI.Text
---@field m_Button Framework.UI.Button
TestUI = class("CustomGame.UI.TestUI", super)

---OnComplete
---@param ui UnityEngine.GameObject
function TestUI:OnComplete(ui)
    super.OnComplete(self, ui)
    self.m_Text.text = "hello tolua"
    self.m_Button:AddEventListener("click", self.onClick)
end

function TestUI:GetAssetPath()
    if IsEditor then
        return "Assets/prefabs/TestUI.prefab"
    else
        return "2"
    end
end

function TestUI:onClick(...)
    LogUtil.LogError("onclick")
end

return TestUI
