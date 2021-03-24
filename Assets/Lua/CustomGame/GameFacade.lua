---
--- Created by hepengqi.
--- DateTime: 2021/3/19 17:04
---

require("Framework.event.Event")

---@type Facade
local super = require("puremvc.patterns.facade.Facade")

---@class GameFacade:Facade
GameFacade = class("CustomGame.GameFacade", super)

function GameFacade:Start()
    self:registerFus()
    self:sendNotification("TestNotify", TestMediator.__cname)
end

function GameFacade:registerFus()
    require("CustomGame.fu.testfu.TestImpl")
end

return GameFacade