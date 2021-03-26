super = require("puremvc.patterns.mediator.Mediator")

---@class Framework.core.mvc.GameMediator:Mediator
GameMediator = class("Framework.core.mvc.GameMediator", super)
GameMediator.NAME = ""
function GameMediator:ctor()
    self.class.NAME = self.__cname
    super.ctor(self, self.__cname)
end

function GameMediator:show()
    if self.viewComponent == nil then
        local cls = self:getUIClass()
        local ui = cls.New()
        self:setViewComponent(ui)
        ui:AddEventListener("Complete", self, self.onUILoaded)
        ui:LoadResource()
    else
        self:showUI()
    end
end

---onUILoaded
---@param evt Framework.event.Event
function GameMediator:onUILoaded(evt)
    self:showUI()
end

function GameMediator:showUI()
    
end

---@return Framework.UI.Prefab
function GameMediator:getUIClass()
    error("must override")
end

return GameMediator