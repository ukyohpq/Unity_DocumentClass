----------------------------- 以下为 UI代码 不可修改 -----------------------------------
require("Examples.05_DocumentClass.UI_Doc")
require("Examples.05_DocumentClass.UI_Doc")
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.05_DocumentClass.UI_05:Framework.UI.Prefab
---@field m1_Doc Examples.05_DocumentClass.UI_Doc
---@field m2_Doc Examples.05_DocumentClass.UI_Doc
UI_05 = class("Examples.05_DocumentClass.UI_05", super)

function UI_05:ctor(autoBind)
    super.ctor(self, autoBind)
	self.m1_Doc = UI_Doc.New(false)
	self:AddChild(self.m1_Doc)
	self.m2_Doc = UI_Doc.New(false)
	self:AddChild(self.m2_Doc)
end

function UI_05:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/05_DocumentClass/UI_05.prefab"
    else
        return "2"
    end
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return UI_05
