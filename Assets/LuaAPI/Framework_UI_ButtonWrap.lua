---@class Framework.UI.Button : UnityEngine.MonoBehaviour
local m = {}
---@param eventName string
---@param self LuaInterface.LuaTable
---@param handler LuaInterface.LuaFunction
function m:AddEventListener(eventName, self, handler) end
---@param eventName string
---@param self LuaInterface.LuaTable
---@param handler LuaInterface.LuaFunction
function m:RemoveEventListener(eventName, self, handler) end
---@param eventName string
function m:DispatchEvent(eventName) end
Framework = {}
Framework.UI = {}
Framework.UI.Button = m
return m