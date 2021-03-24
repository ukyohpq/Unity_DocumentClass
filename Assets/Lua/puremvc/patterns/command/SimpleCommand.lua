local super= Notifier
---@class SimpleCommand:Notifier
SimpleCommand = class("puremvc.patterns.command.SimpleCommand", super)

function SimpleCommand:ctor()
    super.ctor(self)
end

---execute
---@param notification Notification
function SimpleCommand:execute(notification)
	
end

return SimpleCommand