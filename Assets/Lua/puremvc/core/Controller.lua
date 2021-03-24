---@class Controller
---@field view View
---@field commandMap table<string, SimpleCommand>
Controller = class("puremvc.core.Controller")

local SINGLETON_MSG = "Controller Singleton already constructed!"

---@type Controller
local instance

function Controller.getInstance()
	if instance == nil then
		instance = Controller.new()
	end
	return instance
end

function Controller:initializeController()
    self.view = View:getInstance()
end

function Controller:ctor()
	if instance ~= nil then
		error(SINGLETON_MSG)
	end
	self.commandMap = {}
	self:initializeController()
end

---executeCommand
---@param note Notification
function Controller:executeCommand(note)
	local commandClassRef = self.commandMap[note:getName()]
	if commandClassRef == nil then return end
    local commandInstance = commandClassRef.new()
    commandInstance:execute(note)
end

---registerCommand
---@param notificationName string
---@param commandClassRef SimpleCommand
function Controller:registerCommand(notificationName, commandClassRef)
	if self.commandMap[notificationName] == nil then
	   self.view:registerObserver(notificationName, require("puremvc.patterns.observer.Observer").new(self.executeCommand, self))
	end
	self.commandMap[notificationName] = commandClassRef
end

---hasCommand
---@param notificationName string
function Controller:hasCommand(notificationName)
	return self.commandMap[notificationName] ~= nil
end

---removeCommand
---@param notificationName string
function Controller:removeCommand(notificationName)
	if self:hasCommand(notificationName) then
	   self.view:removeObserver(notificationName, self)
	   self.commandMap[notificationName] = nil
	end
end

return Controller