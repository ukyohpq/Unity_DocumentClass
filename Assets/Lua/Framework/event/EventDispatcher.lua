local emptyCaller = {}



---@class Framework.event.EventDispatcher
---@field private nameMap
EventDispatcher = class("Framework.event.EventDispatcher")

function EventDispatcher:ctor()
    self:reset()
end

---DispatchEvent
---@param evt Framework.event.Event
function EventDispatcher:DispatchEvent(evt)
    evt:SetCurrentTarget(self)
    local eventName = evt:GetEventName()
    local callerMap = self.nameMap[eventName]
    if callerMap == nil then
        callerMap = {}
        self.nameMap[eventName] = callerMap
    end
    for caller, handlerMap in pairs(callerMap) do
        for handler, _ in pairs(handlerMap) do
            if caller == emptyCaller then
                handler(evt)
            else
                handler(caller, evt)
            end
        end
    end
end

function EventDispatcher:DispatchMessage(eventName, ...)
    local evt = Event.New(eventName)
    evt:SetTarget(self)
    evt.data = {...}
    self:DispatchEvent(evt)
end

function EventDispatcher:HasEventHandler(eventName, caller)
    local callerMap = self.nameMap[eventName]
    if callerMap == nil then
        return false
    end
    local curCaller = caller or emptyCaller
    return callerMap[curCaller] ~= nil
end

function EventDispatcher:AddEventListener(eventName, caller, handler)
    local callerMap = self.nameMap[eventName]
    if callerMap == nil then
        callerMap = {}
        self.nameMap[eventName] = callerMap
    end
    local curCaller = caller or emptyCaller
    local handlerMap = callerMap[curCaller]
    if handlerMap == nil then
        handlerMap = {}
        callerMap[curCaller] = handlerMap
    end
    if handlerMap[handler] ~= nil then
        return
    end
    handlerMap[handler] = 1
end

function EventDispatcher:RemoveEventListener(eventName, caller, handler)
    local callerMap = self.nameMap[eventName]
    if callerMap == nil then
        return
    end
    local curCaller = caller or emptyCaller
    local handlerMap = callerMap[curCaller]
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