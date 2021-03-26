
NotificationName = {}
--global
NotificationName.SHOW_SCENE = "SHOW_SCENE"
NotificationName.POPUP_WINDOW = "POPUP_WINDOW"


--testfu
NotificationName.INCREASE_CLICK = "INCREASE_CLICK"


--editor check
--统一在这里检测有没有重复定义的通知名称
--没有把通知名称用模块分开，也是为了避免重复字符串的出现
local function checkDuplicateName()
    local mapV = {}
    for k, v in pairs(NotificationName) do
        if mapV[v] then
            error(string.format("duplicate NotificationName k:%s v:%s", k, v))
        end
        mapV[v] = true
    end
end

if IsEditor then
    checkDuplicateName()
end

return NotificationName