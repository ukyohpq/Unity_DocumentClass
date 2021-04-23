require("Examples.01_SimpleUI.UI_01")

---@class Examples.01_SimpleUI.logic_01
logic_01 = class("Examples.01_SimpleUI.logic_01")

function logic_01:ctor()
    local ui = UI_01.New()
end

return logic_01