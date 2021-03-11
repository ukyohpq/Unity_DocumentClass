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

--主入口函数。从这里开始lua逻辑
function Main(isEditor)
	IsEditor = isEditor
	requireLua(isEditor)
	--LogUtil.LogError("Main start isEditor:%s", isEditor)+
	cap = UnityEngine.GameObject.New()
	LogUtil.LogError("=================cap:%s", cap)
	mainLogic = MainLogic.New()
	--require("UnityEngine.GameObject")
	--require("UnityEngine.UI.Text")
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

function CSCallLua(evtName, contextID, uiPrefab)
	LogUtil.LogError("evtName:%s contextID:%s prefab:%s", evtName, contextID, uiPrefab)
	prefabComplete(contextID, uiPrefab)
end