---@class Observer
---@field notify fun(string, Notification):void
---@field notifyContext string
Observer = class("puremvc.patterns.observer.Observer")

---ctor
---@param notifyMethod fun()
---@param notifyContext table
function Observer:ctor(notifyMethod, notifyContext)
	self:setNotifyMethod(notifyMethod)
	self:setNotifyContext(notifyContext)
end

function Observer:setNotifyMethod(notifyMethod)
	self.notify = notifyMethod
end

function Observer:setNotifyContext(notifyContext)
	self.context = notifyContext
end

function Observer:getNotifyMethod()
	return self.notify
end

function Observer:getNotifyContext()
	return self.context
end

function Observer:notifyObserver(notification)
	self:getNotifyMethod()(self:getNotifyContext(), notification)
end

function Observer:compareNotifyContext(object)
	return object == self.context
end

return Observer