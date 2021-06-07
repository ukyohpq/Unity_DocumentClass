local super = require("Framwork.display.DisplayObject")

---@class Framework.display.DisplayObjectContainer:Framework.display.DisplayObject
---@field private children Framework.display.DisplayObject[]
DisplayObjectContainer = class("Framework.display.DisplayObjectContainer", super)

function DisplayObjectContainer:ctor()
    self.children = {}
end

---AddChild
---@param child Framework.display.DisplayObject
function DisplayObjectContainer:AddChild(child)
    table.insert(self.children, child)
end

function DisplayObjectContainer:AddChildAt(child, index)
    table.insert(self.children, index, child)
end

function DisplayObjectContainer:GetNumChildren()
    return #self.children
end

function DisplayObjectContainer:GetChildAt(index)
    return self.children[i]
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