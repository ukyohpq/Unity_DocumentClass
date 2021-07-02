---@class Framework.event.Delegate
---@field dispatcher Framework.event.EventDispatcher
---@field eventName string
Delegate = class("Framework.event.Delegate")

---ctor
---@param eventName string
function Delegate:ctor(eventName)
    self.eventName = eventName
    self.dispatcher = EventDispatcher.New()
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
    self.dispatcher:RemoveAllEventListeners()    
end

return Delegate