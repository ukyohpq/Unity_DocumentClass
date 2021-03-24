require("puremvc.core.Model")
require("puremvc.core.Controller")
require("puremvc.core.View")
require("puremvc.patterns.observer.Notification")
require("puremvc.patterns.observer.Notifier")

---@class Facade
---@field private model Model
---@field private controller Controller
---@field private view View
Facade = class("src.puremvc.patterns.facade.Facade")

local SINGLETON_MSG = "Facade Singleton already constructed!"

---@type Facade
local instance
---@return Facade
function Facade.getInstance()
    if instance == nil then
        instance = Facade.new()
    end
    return instance
end

function Facade:initializeModel()
    if self.model == nil then
        self.model = Model.getInstance()
    end
end

function Facade:initializeController()
    if self.controller == nil then
        self.controller = Controller.getInstance()
    end
end

function Facade:initializeView()
    if self.view == nil then
        self.view = View.getInstance()
    end
end

function Facade:initializeFacade()
    self:initializeModel()
    self:initializeController()
    self:initializeView()
end

function Facade:ctor()
    if instance ~= nil then
        error(SINGLETON_MSG)
    end
	self:initializeFacade()
end

---registerCommand
---@param notificationName string
---@param commandClassRef table
function Facade:registerCommand(notificationName, commandClassRef)
    self.controller:registerCommand(notificationName, commandClassRef)
end

---removeCommand
---@param notificationName string
function Facade:removeCommand(notificationName)
    self.controller:removeCommand(notificationName)
end

---hasCommand
---@param notificationName string
function Facade:hasCommand(notificationName)
	return self.controller:hasCommand(notificationName)
end

---registerProxy
---@param proxy Proxy
function Facade:registerProxy(proxy)
	self.model:registerProxy(proxy)
end

---retrieveProxy
---@param proxyName string
function Facade:retrieveProxy(proxyName)
	return self.model:retrieveProxy(proxyName)
end

---removeProxy
---@param proxyName string
function Facade:removeProxy(proxyName)
    local proxy
    if self.model ~= nil then
        proxy = self.model:removeProxy(proxyName)
    end
    return proxy
end

---hasProxy
---@param proxyName string
function Facade:hasProxy(proxyName)
	return self.model:hasProxy(proxyName)
end

---registerMediator
---@param mediator Mediator
function Facade:registerMediator(mediator)
	if self.view ~= nil then
	   self.view:registerMediator(mediator)
	end
end

---retrieveMediator
---@param mediatorName string
function Facade:retrieveMediator(mediatorName)
	return self.view:retrieveMediator(mediatorName)
end


---removeMediator
---@param mediatorName string
function Facade:removeMediator(mediatorName)
    ---@type Mediator
    local mediator
    if self.view ~= nil then 
        mediator = self.view:removeMediator(mediatorName)
    end
    return mediator
end

---hasMediator
---@param mediatorName string
function Facade:hasMediator(mediatorName)
	return self.view:hasMediator(mediatorName)
end

---sendNotification
---@param notificationName string
---@param body table
---@param type table
function Facade:sendNotification(notificationName, body, type)
    self:notifyObservers(Notification.new(notificationName, body, type))
end

---notifyObservers
---@param notification Notification
function Facade:notifyObservers(notification)
	if self.view ~= nil then
	   self.view:notifyObservers(notification)
	end
end

return Facade