------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.07_ImageWithAtlas.UI_07:Framework.UI.Prefab
---@field m_Image Framework.UI.Image
---@field m_Button Framework.UI.Button
UI_07 = class("Examples.07_ImageWithAtlas.UI_07", super)

function UI_07:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/07_ImageWithAtlas/UI_07.prefab"
    else
        return "2"
    end
end

return UI_07
