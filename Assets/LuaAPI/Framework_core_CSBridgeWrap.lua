---@class Framework.core.CSBridge : object
local m = {}
---@param path string
---@param luaTable LuaInterface.LuaTable
function m.LoadPrefab(path, luaTable) end
Framework = {}
Framework.core = {}
Framework.core.CSBridge = m
return m