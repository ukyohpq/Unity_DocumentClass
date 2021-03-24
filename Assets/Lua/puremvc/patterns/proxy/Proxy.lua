local super = Notifier
---@class Proxy:Notifier
Proxy = class("puremvc.patterns.proxy", super)
local NAME = "Proxy"

function Proxy:ctor(proxyName, data)
    super.ctor(self)
	if proxyName == nil then
	   self.proxyName = NAME
	else
       self.proxyName = proxyName
	end
	
	if data ~= nil then
	   self:setData(data)
	end
end

function Proxy:setData(data)
	self.data = data
end

function Proxy:getProxyName()
	return self.proxyName
end

function Proxy:getData()
	return self.data
end

function Proxy:onRegister()
	
end

function Proxy:onRemove()
	
end

return Proxy