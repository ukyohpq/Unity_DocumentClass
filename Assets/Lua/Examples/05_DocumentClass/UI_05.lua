------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

require("Examples.05_DocumentClass.UI_Doc")
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.05_DocumentClass.UI_05:Framework.UI.Prefab
---@field m_Doc Examples.05_DocumentClass.UI_Doc
UI_05 = class("Examples.05_DocumentClass.UI_05", super)

function UI_05:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/05_DocumentClass/UI_05.prefab"
    else
        return "2"
    end
end

return UI_05