local super = require("puremvc.patterns.proxy.Proxy")

---@class CustomGame.fu.testfu.TestProxy:Proxy
TestProxy = class("CustomGame.fu.testfu.TestProxy", super)

function TestProxy:ctor()
    super.ctor(self, self.__cname, {})
end

return TestProxy