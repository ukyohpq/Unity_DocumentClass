---@class Framework.event.Event
---@field private target
---@field private currentTarget
---@field private name string
---@field private data table
---@field private isBubble boolean
Event = class("Framework.event.Event")
Event.COMPLETE = "COMPLETE"
Event.INIT = "INIT"
Event.ON_BIND = "OnBind"


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

function Event:StopBubble()
    self.isBubble = false
end

function Event:IsBubble()
    return self.isBubble
end