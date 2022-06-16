---
--- Created by mengchengbo.
--- DateTime: 2019/7/2 16:27
--- 游戏公共设置（配置）


--- @class GameSettings
local GameSettings =
{
    LOG_LEVEL = 1
}

function SetLogLevel(level)
    FLua.Log.BTLog.level = level
    print = function() end
    GameSettings.LOG_LEVEL = level
    if level > ELogType.Debug then
        LogUtil.Debug = function() end
       
    end

    if level > ELogType.Info then
        LogUtil.Log = function() end
 		LogInfo =  LogUtil.Debug
    end

    if level > ELogType.Warning then
        LogUtil.LogWarning = function() end
        LogWarning = LogUtil.LogWarning
        traceTable = function() return "" end
    end

    if level > ELogType.Error then
        LogUtil.LogError = function() end
        LogError = LogUtil.LogError
    end

    if level > ELogType.Assert then
        assert = function() end
    end

    if level > ELogType.Exception then
        LogUtil.LogException = function() end
        LogException = LogUtil.LogException
    end
end

return GameSettings