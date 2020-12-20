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
	--LogUtil.Debug("Main start")
	requireLua(isEditor)
	mainLogic = MainLogic.New()
	LogUtil.LogError("isEditor:%s", isEditor)
end

function Update(deltaTime, unscaledDeltaTime)
	mainLogic:Update(deltaTime, unscaledDeltaTime)
end

function FixedUpdate(fixedDeltaTime, fixedUnscaledTime)
	mainLogic:fixedUpdate(fixedDeltaTime, fixedUnscaledTime)
end