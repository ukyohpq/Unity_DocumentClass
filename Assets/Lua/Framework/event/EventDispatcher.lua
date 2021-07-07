require("Framework.event.Event")

local emptyOwner = {}
---@class EventHandler
---@field owner table
---@field handler fun(evt:Framework.event.Event)
local EventHandler = class("EventHandler")
function EventHandler:ctor(owner, handler)
    self.owner = owner
    self.handler = handler
end

---call
---@param evt Framework.event.Event
function EventHandler:call(evt)
    if self.owner then
        self.handler(self.owner, evt)
    else
        self.handler(evt)
    end
end

function EventHandler:Equals(owner, handler)
    return (self.owner == owner) and (self.handler == handler)
end

---@class Framework.event.EventDispatcher
---@field private nameMap table<string, EventHandler[]>
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
    local handlerMap = self.nameMap[eventName]
    if handlerMap == nil then
        handlerMap = {}
        self.nameMap[eventName] = handlerMap
    end
    for _, eventHandler in ipairs(handlerMap) do
        eventHandler:call(evt)
    end
end

function EventDispatcher:DispatchMessage(eventName, ...)
    local evt = Event.New(eventName)
    evt.data = {...}
    self:DispatchEvent(evt)
end

function EventDispatcher:HasEventHandler(eventName)
    return self.nameMap[eventName] ~= nil
end

function EventDispatcher:AddEventListener(eventName, handlerOwner, handler)
    if handler == nil then
        return
    end
    local handlerMap = self.nameMap[eventName]
    if handlerMap == nil then
        handlerMap = {}
        self.nameMap[eventName] = handlerMap
    end
    for _, v in ipairs(handlerMap) do
        if v:Equals(handlerOwner, handler) then
            return
        end
    end
    table.insert(handlerMap, EventHandler.New(handlerOwner, handler))
end

function EventDispatcher:RemoveEventListener(eventName, handlerOwner, handler)
    if handler == nil then
        return
    end
    local ownerMap = self.nameMap[eventName]
    if ownerMap == nil then
        return
    end
    for i, v in ipairs(ownerMap) do
        if v:Equals(handlerOwner, handler) then
            table.remove(ownerMap, i)
            break
        end 
    end
end

function EventDispatcher:RemoveAllEventListeners()
    self:reset()
end

---reset
---@private
function EventDispatcher:reset()
    self.nameMap = {}
end

---RemoveAllEventsByName
---@param eventName string
function EventDispatcher:RemoveAllEventsByName(eventName)
    self.nameMap[eventName] = nil
end

return EventDispatcher