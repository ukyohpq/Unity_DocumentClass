local super = require("Framework.display.InterActiveObject")

---@class Framework.display.DisplayObjectContainer:Framework.display.InterActiveObject
---@field private children Framework.display.DisplayObject[]
DisplayObjectContainer = class("Framework.display.DisplayObjectContainer", super)

function DisplayObjectContainer:ctor()
    super.ctor(self)
    self.children = {}
end

---InsertChild 仅在lua端建立父子关系，不影响unity端
---@param child Framework.display.InterActiveObject
function DisplayObjectContainer:InsertChild(child)
    return self:InsertChildAt(child, #self.children + 1)
end

---InsertChildAt
---@param child Framework.display.InterActiveObject
---@param index number
function DisplayObjectContainer:InsertChildAt(child, index)
    if child.parent == self then
        return false
    end
    child.parent = self
    table.insert(self.children, index, child)
    return true
end

---AddChild 不仅在lua端建立父子关系，也为unity端指定了父transform
---@param child Framework.display.DisplayObject
function DisplayObjectContainer:AddChild(child)
    return self:AddChildAt(child, #self.children + 1)
end

---AddChildAt
---@param child Framework.display.DisplayObject
---@param index number
function DisplayObjectContainer:AddChildAt(child, index)
    if not self:InsertChildAt(child, index) then
        return
    end
    child.parentTransform = true
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

function DisplayObjectContainer:GetChildIndex(child)
    for i, v in ipairs(self.children) do
        if v == child then
            return i
        end
    end
    return -1
end

function DisplayObjectContainer:GetChildByName(name)
    --TODO
end

---RemoveChild
---@param child Framework.display.DisplayObject
function DisplayObjectContainer:RemoveChild(child)
    for i, v in ipairs(self.children) do
        if v == child then
            table.remove(self.children, i)
            child.parent = nil
            child.parentTransform = false
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
    child.parent = nil
    child.parentTransform = false
    return child
end

return DisplayObjectContainer