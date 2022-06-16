---
--- Created by MengChengBo
--- DateTime: 2017/10/31 10:47
---
local GameSettings = require("Framework.Setting")
local _log = FLua.Log.BTLog
--------------------
ELogType =
{
    Debug = 0,    --- 调试信息的打印，日志等级最低
Info = 1,     --- 基本的日志输出(不带trace)
    Warning = 2,  ---
    Error = 3,
    Assert = 4,
    Exception = 5,
}


-- 判断日志等级
local function IsMuted(logLevel)
    return logLevel < GameSettings.LOG_LEVEL
end


local __build_log_concat__ = function(tb)
    local str = ""
    for i = 1, #tb do
        str = str .. tostring(tb[i]) .. "\t"
    end
    return str
end



local __log_system_list__ = {}
Logger = {}
function Logger.RegisterSystem(system, switch)
    __log_system_list__[system] = switch
end

LogBuilder = {}
function LogBuilder.Write(...)
    return __build_log_concat__({ ... })
end

function LogBuilder.WriteF(fmt, ...)
    --默认变量数量不会超过20个
    local length = 20
    local p = {...}
    p[length] = 0
    for i = 1, length do
        local v = p[i]
        if v == nil then
            p[i] = "nil"
        elseif type(v) == "table" then
            p[i] = traceTable(v, true, 5)
        elseif type(v) ~= "number" then
            p[i] = tostring(v)
        end
    end

    return string.format(fmt, unpack(p))
end

function LogInfo(system, fmt, ...)
    if __log_system_list__[system] == false then
        return
    end
    LogUtil.Log( fmt, ...)
end
function LogWarning(system, fmt, ...)
    if __log_system_list__[system] == false then
        return
    end
    LogUtil.LogWarning( fmt, ...)
end


function LogError(system, fmt, ...)
    if __log_system_list__[system] == false then
        return
    end
    LogUtil.LogError( fmt, ...)
end


function LogException(system, fmt, ...)
    if __log_system_list__[system] == false then
        return
    end
    LogUtil.LogException(fmt,...)
end

-----------------------------------------------------------

local function simpleLog(isTrace, log, ...)
    local log_str = LogBuilder.WriteF(log, ...)
    if isTrace then
        local stack = debug.traceback()
        return log_str .. "\n" .. stack
    else
        return log_str
    end
end



LogUtil = {}
---- 调试信息专用接口
function LogUtil.Debug(log, ...)
    if IsMuted(ELogType.Debug) then
        return
    end
    _log.D(simpleLog(true, log, ...))
end

function LogUtil.Log(log, ...)
    if IsMuted(ELogType.Info) then
        return
    end

    _log.I(simpleLog(false,log, ...))
end

function LogUtil.LogWarning(log, ...)
    if IsMuted(ELogType.Warning) then
        return
    end
    _log.W(simpleLog(true, log, ...))
end

function LogUtil.LogError(log, ...)
    if IsMuted(ELogType.Error) then
        return
    end

    _log.E(simpleLog(true, log, ...))
end

function LogUtil.LogException(log, ...)
    if IsMuted(ELogType.Exception) then
        return
    end
    _log.Exception(simpleLog(true, log, ...))
end

function LogUtil.PCallException(log)
    _log.Exception(log)
end


function LogUtil.OnLineInfo(log, ...)
    _log.OnLineInfo(simpleLog(true, log, ...))
end

function LogUtil.SetLogLevel(level)
    GameSettings.LOG_LEVEL = level
    _log.level = level


end


local _assert = assert
assert = function(...)
    if IsMuted(ELogType.Assert) then
        return
    end
    _assert(...)
end

local json = require 'cjson'
function LogUtil.SdkAddTags(serverId, roleID, roleName)
    local data = {}
    data.serverId = tostring(serverId)
    data.roleId = tostring(roleID)
    data.roleName = tostring(roleName)
    data.scriptVersion=  VersionHandle.GetScriptVersion()
    local jsonStr = json.encode(data)
    --LogUtil.LogError("LogUtil.SdkAddTags   %s", jsonStr)
end


---traceTable
---@param tb table @要输出的table
---@param printTableAddress boolean @如果value是table，是否输出table的地址
---@param depthMax number @最大输出深度
function traceTable(tb, printTableAddress, depthMax)
    if tb == nil then
        return "nil"
    end
    if depthMax == nil or depthMax < 0 then
        depthMax = -1
    end
    --记录已经trace过的table，避免循环引用导致trace死循环
    local traceMap = {}
    traceMap[tb] = "."
    local function fun(tb, space, name, depth)
        local nextSpace = space .. "    "
        local ret = ""
        if depthMax >= 0 and depth >= depthMax then
            ret = ret .. tostring(tb)
        else
            local typestr = type(tb)
            if typestr == "table" then
                if printTableAddress then
                    ret = ret .. tostring(tb)
                end
                ret = ret .. "\n" .. space .. "{\n"
                for k, v in pairs(tb) do
                    local newname = name .. "." .. tostring(k)
                    local valstr = ""
                    if traceMap[v] ~= nil then
                        valstr = "-->(" .. traceMap[v] .. ")\n"
                    else
                        if type(v) == "table" then
                            traceMap[v] = newname
                        end
                        valstr = fun(v, nextSpace, newname, depth+1)
                    end
                    ret = ret .. nextSpace .. "[" .. tostring(k) .. "]" .. ":" .. valstr
                end
                ret = ret .. space .. "}"
            else
                ret = ret .. tostring(tb)
            end
        end
        return ret .. "\n"
    end
    return fun(tb, "", "", 0)
end
