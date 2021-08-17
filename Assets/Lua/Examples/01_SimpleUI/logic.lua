require("Examples.01_SimpleUI.UI_01")

---@class Examples.01_SimpleUI.logic_01
logic_01 = class("Examples.01_SimpleUI.logic_01")

function logic_01:ctor()
    local ui = UI_01.New()
    --ui:SetActive(false)
    local loader = package.loaders[2]
    package.loaders[1] = function(module)
        local ret = loader(module)
        LogUtil.LogError("myloader:%s", ret)
        return ret
    end
    require("Examples.01_SimpleUI.a")
end

return logic_01