require("CustomGame.fu.testfu.TestMediator")
require("CustomGame.fu.testfu.TestProxy")
require("CustomGame.fu.testfu.TestCmd")

GameFacade.getInstance():registerMediator(TestMediator.New())
GameFacade.getInstance():registerProxy(TestProxy.New())
GameFacade.getInstance():registerCommand("TestNotify", TestCmd)