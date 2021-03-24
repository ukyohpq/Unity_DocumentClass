---@class Notification
---@field name string
---@field body table
---@field type string
Notification = class("puremvc.patterns.observer.Notification")

---ctor
---@param name string
---@param body table
---@param type string
function Notification:ctor(name, body, type)
	self.name = name
	self.body = body
	self.type = type
end

function Notification:getName()
	return self.name
end

function Notification:setBody(body)
	self.body = body
end

function Notification:getBody()
	return self.body
end

function Notification:setType(type)
	self.type = type
end

function Notification:getType()
	return self.type
end

function Notification:toString()
	local msg = "Notification Name: " .. self:getName()
    if self.body == nil then
	   msg = msg .. "\nBody:nil"
	else
	   msg = msg .. "\nBody:" .. tostring(self.body)
--	   msg = msg .. "\nBody:" .. self.body:toString()
	end
	
    if self.type == nil then
	   msg = msg .. "\nType:nil"
	else
	   msg = msg .. "\nType:" .. self.type
	end
end

return Notification