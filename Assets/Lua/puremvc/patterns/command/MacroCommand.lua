local super = Notifier
---@class MacroCommand:Notifier
MacroCommand = class("puremvc.patterns.command.MacroCommand", super)
function MacroCommand:initializeMacroCommand()
	
end

function MacroCommand:ctor()
    super.ctor(self)
	self.subCommands = {}
    self:initializeMacroCommand()
end

function MacroCommand:addSubCommand(commandClassRef)
    local table = self.subCommands
	table[#table + 1] = commandClassRef
end

function MacroCommand:execute(notification)
    local tb = self.subCommands
	while #tb do
		local commandClassRef = table.remove(tb, tb[#tb])
		local commandInstance = commandClassRef.new()
		commandInstance:execute(notification)
	end
end

return MacroCommand