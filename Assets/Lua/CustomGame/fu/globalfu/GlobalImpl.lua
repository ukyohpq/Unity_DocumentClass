require("CustomGame.fu.globalfu.ShowSceneCmd")
require("CustomGame.fu.globalfu.SceneManagerProxy")


Facade.getInstance():registerProxy(SceneManagerProxy.New())
Facade.getInstance():registerCommand(NotificationName.SHOW_SCENE, ShowSceneCmd)