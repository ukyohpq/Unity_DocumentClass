---@class Framework.event.Delegate
---@field eventName string
---@field dispatcher Framework.event.EventDispatcher
Delegate = class("Framework.event.Delegate")

---ctor
---@param eventName string
---@param dispatcher Framework.event.EventDispatcher
function Delegate:ctor(eventName, dispatcher)
    self.eventName = eventName
    self.dispatcher = dispatcher
end

---Add
---@param owner table
---@param handler fun(evt:Framework.event.Event)
function Delegate:Add(owner, handler)
    self.dispatcher:AddEventListener(self.eventName, owner, handler)
end

---Remove
---@param owner table
---@param handler fun(evt:Framework.event.Event)
function Delegate:Remove(owner, handler)
    self.dispatcher:RemoveEventListener(self.eventName, owner, handler)
end

function Delegate:Clear()
    self.dispatcher:RemoveAllEventsByName(self.eventName)    
end

return Delegate