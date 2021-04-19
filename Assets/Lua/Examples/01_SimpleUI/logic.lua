require("Examples.01_SimpleUI.UI_01")

---@class Examples.01_SimpleUI.logic
logic = class("Examples.01_SimpleUI.logic")

function logic:ctor()
    local ui = UI_01.New()
end

return logic