----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.01_SimpleUI.UI_01:Framework.UI.Prefab
UI_01 = class("Examples.01_SimpleUI.UI_01", super)

function UI_01:ctor(autoBind)
    super.ctor(self)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function UI_01:GetAssetPath()
    if IsEditor then
        return "Assets/UI/Prefab/Example/UI_01.prefab"
    else
        return "2"
    end
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return UI_01
