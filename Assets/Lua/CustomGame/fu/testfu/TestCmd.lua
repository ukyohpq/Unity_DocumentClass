local super = SimpleCommand

---@class TestCmd:SimpleCommand
TestCmd = class("CustomGame.fu.testfu.TestCmd", super)

function TestCmd:execute(notification)
    LogUtil.LogError("TestCmd execute")
end

return TestCmd