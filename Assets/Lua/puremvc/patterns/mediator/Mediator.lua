local super = Notifier
---@class Mediator:Notifier
Mediator = class("puremvc.patterns.mediator.Mediator", super)

local NAME = "Mediator"

---ctor
---@param mediatorName string
---@param viewComponent table
function Mediator:ctor(mediatorName, viewComponent)
    super.ctor(self)
	if mediatorName ~= nil then
		self.mediatorName = mediatorName
	else
		self.mediatorName = NAME
	end
	self.viewComponent = viewComponent
end

function Mediator:getMediatorName()
	return self.mediatorName
end

function Mediator:setViewComponent(viewComponent)
	self.viewComponent = viewComponent
end

function Mediator:getViewComponent()
	return self.viewComponent
end

function Mediator:listNotificationInterests()
	return {}
end

---handleNotification
---@param notification Notification
function Mediator:handleNotification(notification)
	
end

function Mediator:onRegister()
	
end

function Mediator:onRemove()
	
end

return Mediator