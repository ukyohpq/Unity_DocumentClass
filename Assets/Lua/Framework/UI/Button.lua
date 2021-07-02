local super = require("Framework.display.DisplayObject")

---@class Framework.UI.Button:Framework.display.DisplayObject
Button = class("Framework.UI.Button", super)

---AddClickEvent
---@param handler function()
---@param handlerOwner table
function Button:AddClickEvent(handler, handlerOwner)
    self:AddEventListener("click", handlerOwner, handler)
end

---AddClickEvent
---@param handler fun(evt:Framework.event.Event)
---@param handlerOwner table
function Button:RemoveClickEvent(handler, handlerOwner)
    self:RemoveEventListener("click", handlerOwner, handler)
end

return Button