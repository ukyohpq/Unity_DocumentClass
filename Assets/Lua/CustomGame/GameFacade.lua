---
--- Created by hepengqi.
--- DateTime: 2021/3/19 17:04
---

require("Framework.event.Event")
require("CustomGame.NotificationName")
require("puremvc.patterns.facade.Facade")
require("Framework.core.mvc.GameMediator")
require("Framework.core.mvc.GameProxy")
require("puremvc.patterns.command.SimpleCommand")

---@type Facade
local super = Facade

---@class GameFacade:Facade
GameFacade = class("CustomGame.GameFacade", super)

function GameFacade:Start()
    self:registerFus()
    ---@type CustomGame.fu.globalfu.SceneManagerProxy
    local sceneProxy = self:retrieveProxy(SceneManagerProxy.NAME)
    sceneProxy:showScene(TestMediator.NAME)
    --self:sendNotification(NotificationName.SHOW_SCENE, TestMediator.NAME)
end

function GameFacade:registerFus()
    require("CustomGame.fu.globalfu.GlobalImpl")
    require("CustomGame.fu.testfu.TestImpl")
end

return GameFacade