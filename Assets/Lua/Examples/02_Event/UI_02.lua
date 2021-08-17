----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.02_Event.UI_02:Framework.UI.Prefab
UI_02 = class("Examples.02_Event.UI_02", super)

function UI_02:ctor(autoBind)
    super.ctor(self)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function UI_02:GetAssetPath()
    if IsEditor then
        return "Assets/UI/Prefab/Example/UI_02.prefab"
    else
        return "2"
    end
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return UI_02
