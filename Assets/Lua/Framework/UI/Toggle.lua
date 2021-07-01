local super = DisplayObject

---@class Toggle:Framework.display.DisplayObject
Toggle = class("Toggle", DisplayObject)

function Toggle:ctor()
    
end

---@return boolean
function Toggle:IsOn()
    return false
end

return Toggle