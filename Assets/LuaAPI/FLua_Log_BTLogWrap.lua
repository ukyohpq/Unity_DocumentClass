---@class FLua.Log.BTLog
---@field IsMainThread bool
---@field level int
---@field OnLineDebugInfoOpen bool
local m = {}
---@param message string
function m.D(message) end
---@param message string
function m.I(message) end
---@param message string
function m.OnLineInfo(message) end
---@param format string
---@param args table
function m.LogNetDebugInfo(format, args) end
---@param message string
function m.W(message) end
---@param message string
function m.E(message) end
---@param message string
---@param context UnityEngine.Object
function m.Exception(message, context) end
---@return int
function m.GetLogLevelMacro() end
FLua = {}
FLua.Log = {}
FLua.Log.BTLog = m
return m