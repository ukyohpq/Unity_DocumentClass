---@class Framework.event.Event
---@field private target
---@field private currentTarget
---@field private name string
---@field private data table
Event = class("Framework.event.Event")
Event.COMPLETE = "COMPLETE"


function Event:ctor(name, data)
    self.name = name
    self.data = data
end

function Event:GetTarget()
    return self.target
end

---SetTarget
function Event:SetTarget(target)
    self.target = target
end

function Event:GetCurrentTarget()
    return self.currentTarget
end

---SetCurrentTarget
function Event:SetCurrentTarget(target)
    self.currentTarget = target
end

function Event:GetEventName()
    return self.name
end

function Event:GetEventData()
    return self.data
end