---@type MainLogic
local mainLogic

function requireLuaUIFramework(isEditor)
	--log一定要放在第一个require
	require("Framework.Log")
	require("Framework.class")
	if isEditor then
		require("Framework.NewClassForLuaHotFix")
	end
	--require顺序不要改变，HotFix要位于所有class前
	require("Framework.event.Event")
	require("Framework.UI.Button")
	require("Framework.UI.Prefab")
	require("Framework.UI.Image")
	require("Framework.UI.TextField")
end

--主入口函数。从这里开始lua逻辑
function Main(isEditor, logicPath)
	IsEditor = isEditor
	requireLuaUIFramework(isEditor)
	mainLogic = require(logicPath).New()
end

function LuaBridge(funcName, ...)
	local fun = mainLogic[funcName]
	if fun == nil then
		--LogUtil.LogError("can not find function:%s", funcName)
		return
	end
	if type(fun) ~= "function" then
		--LogUtil.LogError("%s must be function", funcNamec)
		return
	end
	fun(mainLogic, ...)
end