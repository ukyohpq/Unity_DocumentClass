---@class Framework.UI.Button : UnityEngine.MonoBehaviour
local m = {}
---@param eventName string
---@param handler LuaInterface.LuaFunction
function m:AddEventListener(eventName, handler) end
---@param eventName string
function m:DispatchEvent(eventName) end
Framework = {}
Framework.UI = {}
Framework.UI.Button = m
return m