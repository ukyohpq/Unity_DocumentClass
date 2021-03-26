local super = GameProxy

---@class CustomGame.fu.testfu.TestProxy:Framework.core.mvc.GameProxy
---@field numClick number
TestProxy = class("CustomGame.fu.testfu.TestProxy", super)

function TestProxy:getText1()
    
end

function TestProxy:getText2()

end

function TestProxy:increaseClickNum()
    self.numClick = self.numClick + 1
    self:sendNotification(NotificationName.INCREASE_CLICK)
end

return TestProxy