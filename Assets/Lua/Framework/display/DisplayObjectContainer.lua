local super = require("Framework.display.DisplayObject")

---@class Framework.display.DisplayObjectContainer:Framework.display.DisplayObject
---@field private children Framework.display.DisplayObject[]
DisplayObjectContainer = class("Framework.display.DisplayObjectContainer", super)

function DisplayObjectContainer:ctor()
    super.ctor(self)
    self.children = {}
end

---AddChild
---@param child Framework.display.DisplayObject
function DisplayObjectContainer:AddChild(child)
    return self:AddChildAt(child, #self.children + 1)
end

---AddChildAt
---@param child Framework.display.DisplayObject
---@param index number
function DisplayObjectContainer:AddChildAt(child, index)
    if self.children[index] == child then
        return
    end
    child.parent = self
    table.insert(self.children, index, child)
    if self.AddChildExtend then
        self:AddChildExtend(child)
    end
    return child
end

function DisplayObjectContainer:GetNumChildren()
    return #self.children
end

function DisplayObjectContainer:GetChildAt(index)
    return self.children[index]
end

function DisplayObjectContainer:GetChildByName(name)
    --TODO
end

function DisplayObjectContainer:RemoveChild(child)
    for i, v in ipairs(self.children) do
        if v == child then
            table.remove(self.children, i)
            return child
        end
    end
    return child
end

function DisplayObjectContainer:RemoveChildAt(index)
    local child = self.children[index]
    if child == nil then
        return child
    end
    table.remove(self.children, index)
    return child
end

return DisplayObjectContainer