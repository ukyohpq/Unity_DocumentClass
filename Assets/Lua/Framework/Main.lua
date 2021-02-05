---@type MainLogic
local mainLogic

local function requireLua(isEditor)
	--log一定要放在第一个require
	require("Framework.Log")
	require("Framework.class")
	if isEditor then
		require("Framework.NewClassForLuaHotFix")
	end
	require("Framework.MainLogic")
end

local cap
--主入口函数。从这里开始lua逻辑
function Main(isEditor)
	--LogUtil.Debug("Main start")
	requireLua(isEditor)
	mainLogic = MainLogic.New()
	LogUtil.LogError("isEditor:%s", isEditor)
	require("UnityEngine.GameObject")
	cap = UnityEngine.GameObject.New()
end

function LuaBridge(funcName, ...)
	local fun = mainLogic[funcName]
	if fun == nil then
		LogUtil.LogError("can not find function:%s", funcName)
		return
	end
	if type(fun) ~= "function" then
		LogUtil.LogError("%s must be function", funcNamec)
		return
	end
	fun(mainLogic, ...)
end