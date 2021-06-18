------------------------------------------------------------------------------------------
----------
----------    不要修改ui代码！
----------    不要修改ui代码！！
----------    不要修改ui代码！！！
----------
------------------------------------------------------------------------------------------

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.08_Container.Container:Framework.UI.Prefab
Container = class("Examples.08_Container.Container", super)

function Container:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/08_Container/Container.prefab"
    else
        return "2"
    end
end

return Container
