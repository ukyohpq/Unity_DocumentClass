---@class Model
---@field proxyMap table<string, Proxy>
Model = class("puremvc.core.Model")

local SINGLETON_MSG = "Model Singleton already constructed!"

---@type Model
local instance

function Model.getInstance()
	if instance == nil then
		instance = Model.new()
	end
	return instance
end

function Model:initializeModel()
    
end

function Model:ctor()
	if instance ~= nil then
		error(SINGLETON_MSG)
	end
	self.proxyMap = {}
	self:initializeModel(self)
end

---registerProxy
---@param proxy Proxy
function Model:registerProxy(proxy)
	self.proxyMap[proxy:getProxyName()] = proxy
	proxy:onRegister()
end

---retrieveProxy
---@param proxyName string
function Model:retrieveProxy(proxyName)
	return self.proxyMap[proxyName]
end

---hasProxy
---@param proxyName string
function Model:hasProxy(proxyName)
	return self.proxyMap[proxyName] ~= nil
end

---removeProxy
---@param proxyName string
function Model:removeProxy(proxyName)
	local proxy = self.proxyMap[proxyName]
	if proxy ~= nil then
	   self.proxyMap[proxyName] = nil
	   proxy:onRemove()
	end
	return proxy
end

return Model