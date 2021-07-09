---
--- Created by hepengqi.
--- DateTime: 2019-03-14 10:49
--- lua版的defer，和golang的defer功能相同。比较消耗性能，不建议频繁使用
---

local hooked= false
local deferMap = {}
setmetatable(deferMap, {__mode="k"})
---
--- lua版的defer，和golang的defer功能相同
--- 示例:
--- local function foo()
---     local i = 0
---     defer(
---         function()
---             i = i + 1
---             print(1, i)
---         end
---     )
---     i = i + 1
---     defer(
---         function()
---             i = i + 1
---             print(2, i)
---         end
---     )
---     print(3, i)
---     return i
--- end
--- print(foo())
--- 输出:
--- 3   1
--- 2   2
--- 1   3
--- call foo:   1
---@param fun fun()
function defer(fun)
    local infoFunc = debug.getinfo(2).func
    local deferList = deferMap[infoFunc]
    if deferList == nil then
        deferList = {}
        deferMap[infoFunc] = deferList
    end
    table.insert(deferList, 1, fun)
    if hooked then
        return
    end
    hooked = true
    local hook = function()
        local hookInfoFunc = debug.getinfo(2).func
        local list = deferMap[hookInfoFunc]
        if list == nil then
            return
        end
        for _, func in ipairs(list) do
            func()
        end
        deferMap[hookInfoFunc] = {}
        debug.sethook()
    end
    debug.sethook(hook, "r")
end

--local function foo()
--    local i = 0
--    defer(
--        function()
--            i = i + 1
--           print(1, i)
--        end
--    )
--    i = i + 1
--    defer(
--            function()
--                i = i + 1
--                print(2, i)
--        end
--    )
--    print(3, i)
--   return i
--end
--print("call foo:", foo())