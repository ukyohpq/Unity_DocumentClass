require("Framework.event.Event")

local emptyOwner = {}



---@class Framework.event.EventDispatcher
---@field private nameMap table<string, table<Framework.event.EventDispatcher, table<function, number>>>
EventDispatcher = class("Framework.event.EventDispatcher")

function EventDispatcher:ctor()
    self:reset()
end

---DispatchEvent
---@param evt Framework.event.Event
function EventDispatcher:DispatchEvent(evt)
    if evt.target == nil then
        evt:SetTarget(self)
    end
    evt:SetCurrentTarget(self)
    local eventName = evt:GetEventName()
    local ownerMap = self.nameMap[eventName]
    if ownerMap == nil then
        ownerMap = {}
        self.nameMap[eventName] = ownerMap
    end
    for owner, handlerMap in pairs(ownerMap) do
        for handler, _ in pairs(handlerMap) do
            if owner == emptyOwner then
                handler(evt)
            else
                handler(owner, evt)
            end
        end
    end
end

function EventDispatcher:DispatchMessage(eventName, ...)
    local evt = Event.New(eventName)
    evt.data = {...}
    self:DispatchEvent(evt)
end

function EventDispatcher:HasEventHandler(eventName, owner)
    local ownerMap = self.nameMap[eventName]
    if ownerMap == nil then
        return false
    end
    local curOwner = owner or emptyOwner
    return ownerMap[curOwner] ~= nil
end

function EventDispatcher:AddEventListener(eventName, handlerOwner, handler)
    if handler == nil then
        return
    end
    local ownerMap = self.nameMap[eventName]
    if ownerMap == nil then
        ownerMap = {}
        self.nameMap[eventName] = ownerMap
    end
    local curOwner = handlerOwner or emptyOwner
    local handlerMap = ownerMap[curOwner]
    if handlerMap == nil then
        handlerMap = {}
        ownerMap[curOwner] = handlerMap
    end
    if handlerMap[handler] ~= nil then
        return
    end
    handlerMap[handler] = 1
end

function EventDispatcher:RemoveEventListener(eventName, handlerOwner, handler)
    if handler == nil then
        return
    end
    local ownerMap = self.nameMap[eventName]
    if ownerMap == nil then
        return
    end
    local curOwner = handlerOwner or emptyOwner
    local handlerMap = ownerMap[curOwner]
    if handlerMap == nil then
        return
    end
    handlerMap[handler] = nil
end

function EventDispatcher:RemoveAllEventListeners()
    self:reset()
end

---reset
---@private
function EventDispatcher:reset()
    self.nameMap = {}
end

return EventDispatcher