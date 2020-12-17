require("Framework.Log")
require("Framework.class")
require("Framework.NewClassForLuaHotFix")
--主入口函数。从这里开始lua逻辑
function Main()
	--LogUtil.Debug("Main start")	
	LogUtil.LogError("test:%s", class("safsdaf"))
end

function Update(deltaTime, unscaledDeltaTime)
	update(deltaTime, unscaledDeltaTime)
end

function update(deltaTime, unscaledDeltaTime)
	--LogUtil.LogError("Update deltaTime:%s unscaledDeltaTime:%s", deltaTime, unscaledDeltaTime)
end

function FixedUpdate(fixedDeltaTime, fixedUnscaledTime)
	--fixedUpdate(fixedDeltaTime, fixedUnscaledTime)
end

function fixedUpdate(fixedDeltaTime, fixedUnscaledTime)
	LogUtil.LogError("FixedUpdate fixedDeltaTime:%s fixedUnscaledTime:%s", fixedDeltaTime, fixedUnscaledTime)
end
