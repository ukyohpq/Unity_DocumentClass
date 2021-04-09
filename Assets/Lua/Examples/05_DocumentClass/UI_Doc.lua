------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.05_DocumentClass.UI_Doc:Framework.UI.Prefab
---@field m_Button Framework.UI.Button
---@field m_Text UnityEngine.UI.Text
UI_Doc = class("Examples.05_DocumentClass.UI_Doc", super)

function UI_Doc:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/05_DocumentClass/Doc.prefab"
    else
        return "2"
    end
end

return UI_Doc
