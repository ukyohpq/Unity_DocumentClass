---@class Notifier
---@field facade Facade
Notifier = class("puremvc.patterns.observer.Notifier")

function Notifier:ctor()
	self.facade = Facade:getInstance()
end

function Notifier:sendNotification(notificationName, body, type)
	self.facade:sendNotification(notificationName, body, type)
end

return Notifier