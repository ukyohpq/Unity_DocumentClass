local super = require("puremvc.patterns.proxy.Proxy")

---@class Framework.core.mvc.GameProxy:Proxy
GameProxy = class("Framework.core.mvc.GameProxy", super)

function GameProxy:ctor()
    self.class.NAME = self.__cname
    super.ctor(self, self.__cname)
end

return GameProxy