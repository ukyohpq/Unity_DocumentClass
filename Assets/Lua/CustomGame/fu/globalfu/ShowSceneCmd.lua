local super = SimpleCommand

---@class CustomGame.fu.globalCmd.ShowSceneCmd:SimpleCommand
ShowSceneCmd = class("CustomGame.fu.globalCmd.ShowSceneCmd", super)

function ShowSceneCmd:execute(notification)
    ---@type Framework.core.mvc.GameMediator
    local mediator = self.facade:retrieveMediator(notification:getBody())
    mediator:show()
end

return ShowSceneCmd