---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class CustomGame.UI.TestUI:Framework.UI.Prefab
---@field m_Text UnityEngine.UI.Text
---@field m_Button Framework.UI.Button
---@field m2_Text UnityEngine.UI.Text
TestUI = class("CustomGame.UI.TestUI", super)

---OnComplete
---@param evt Framework.event.Event
function TestUI:OnComplete(evt)
end

function TestUI:GetAssetPath()
    if IsEditor then
        return "Assets/prefabs/TestUI.prefab"
    else
        return "2"
    end
end

return TestUI
