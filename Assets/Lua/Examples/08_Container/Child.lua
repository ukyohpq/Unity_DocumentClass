----------------------------- 以下为 UI代码 不可修改 -----------------------------------
---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class Examples.08_Container.Child:Framework.UI.Prefab
---@field m_Button Framework.UI.Button
Child = class("Examples.08_Container.Child", super)

function Child:ctor(autoBind)
    super.ctor(self)
	self.m_Button = Button.New()
	self:AddChild(self.m_Button)
    if autoBind ~= false then
        self:bindExtend()
    end
end

function Child:GetAssetPath()
    if IsEditor then
        return "Assets/Lua/Examples/08_Container/child.prefab"
    else
        return "2"
    end
end

----------------------------- 以下为 逻辑代码 可以修改 -----------------------------------
return Child
