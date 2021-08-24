----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.08_Container.Child:Framework.UI.Prefab
---@field m_Button Framework.UI.Button
Child = class("Examples.08_Container.Child", super)

function Child:ctor(autoBind)
    super.ctor(self)
	self.m_Button = Button.New()
	self:InsertChild(self.m_Button)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function Child:GetAssetPath()
    return "Assets/UI/Prefab/Example/child.prefab"
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return Child
