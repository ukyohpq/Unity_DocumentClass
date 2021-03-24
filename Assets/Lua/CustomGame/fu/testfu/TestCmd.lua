local super = require("puremvc.patterns.command.SimpleCommand")

---@class TestCmd:SimpleCommand
TestCmd = class("CustomGame.fu.testfu.TestCmd", super)

function TestCmd:execute(notification)
    ---@type CustomGame.fu.testfu.TestMediator
    local mediator = self.facade:retrieveMediator(notification:getBody())
    mediator:show()
end

return TestCmd