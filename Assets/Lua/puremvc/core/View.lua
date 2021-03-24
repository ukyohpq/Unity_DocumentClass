---@class View
---@field mediatorMap table<string, Mediator>
View = class("puremvc.core.View")

local SINGLETON_MSG = "View Singleton already constructed!"

---@type View
local instance

function View.getInstance()
	if instance == nil then
		instance = View.new()
	end
	return instance
end

function View:initializeView()
    
end

function View:ctor()
	if instance ~= nil then
		error(SINGLETON_MSG)
	end
	self.mediatorMap = {}
	self.observerMap = {}
	self:initializeView()
end

---registerObserver
---@param notificationName string
---@param observer Observer
function View:registerObserver(notificationName, observer)
    local observers = self.observerMap[notificationName]
    if observers ~= nil then
        observers[#observers] = observer
    else
        self.observerMap[notificationName] = {observer}
    end
end

function View:notifyObservers(notification)
	if self.observerMap[notification:getName()] ~= nil then
	   local observers_ref = self.observerMap[notification:getName()]
	   --把observers从原始数组拷贝到工作数组中
	   --如果不这么做的话，原始数组可能在消息循环进行时发生改变
	   local observers = {}
	   local observer
	   for i=1, #observers_ref do
	       observer = observers_ref[i]
	       observers[i] = observer
	   end
	   
	   for i=1, #observers do
	       observer = observers[i]
	       observer:notifyObserver(notification)	   	
	   end
	end
end

function View:removeObserver(notificationName, notifyContext)
	local observers = self.observerMap[notificationName]
	for i=1, #observers do
		if observers[i]:compareNotifyContext(notifyContext) then
		  table.remove(observers,i)
		  break
		end
	end
	
	if #observers == 0 then
	   self.observerMap[notificationName] = nil 
	end
end

function View:registerMediator(mediator)
    if self.mediatorMap[mediator:getMediatorName()] ~= nil then return end
    self.mediatorMap[mediator:getMediatorName()] = mediator
    local interests = mediator:listNotificationInterests()
    if #interests > 0 then
       local observer = require("puremvc.patterns.observer.Observer").new(mediator.handleNotification, mediator)
       for i=1, #interests do
       	self:registerObserver(interests[i], observer)
       end
    end
    mediator:onRegister()
end

function View:retrieveMediator(mediatorName)
	return self.mediatorMap[mediatorName]
end

function View:removeMediator(mediatorName)
	local mediator = self.mediatorMap[mediatorName]
	if mediator ~= nil then
	   local interests = mediator:listNotificationInterests()
	   for i=1, #interests do
	   	self:removeObserver(interests[i], mediator)
	   	self.mediatorMap[mediatorName] = nil
	   end
	   mediator:onRemove()
	end
	return mediator
end

function View:hasMediator(mediatorName)
	return self.mediatorMap[mediatorName] ~= nil
end

return View