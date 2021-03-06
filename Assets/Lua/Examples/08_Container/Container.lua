----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.08_Container.Container:Framework.UI.Prefab
Container = class("Examples.08_Container.Container", super)

function Container:ctor(autoBind)
    super.ctor(self)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function Container:GetAssetPath()
    return "Assets/UI/Prefab/Example/Container.prefab"
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return Container
