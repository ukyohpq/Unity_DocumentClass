------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class CustomGame.UI.TestUI:Framework.UI.Prefab
---@field m_Text UnityEngine.UI.Text
---@field m_Button Framework.UI.Button
---@field m2_Text UnityEngine.UI.Text
TestUI = class("CustomGame.UI.TestUI", super)

function TestUI:GetAssetPath()
    if IsEditor then
        return "Assets/prefabs/TestUI.prefab"
    else
        return "2"
    end
end

return TestUI
