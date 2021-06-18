------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.08_Container.Child:Framework.UI.Prefab
---@field m_Button Framework.UI.Button
Child = class("Examples.08_Container.Child", super)

function Child:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/08_Container/Child.prefab"
    else
        return "2"
    end
end

return Child
