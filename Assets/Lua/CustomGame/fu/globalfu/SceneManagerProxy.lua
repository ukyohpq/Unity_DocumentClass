local super = GameProxy

---@class CustomGame.fu.globalfu.SceneManagerProxy:Framework.core.mvc.GameProxy
---@field history string[]
---@field curIndex number
---@field maxIndex number
SceneManagerProxy = class("CustomGame.fu.globalfu.SceneManagerProxy", super)

function SceneManagerProxy:ctor()
    super.ctor(self)
    self.history = {}
    self.curIndex = 0
    self.maxIndex = 0
end

---showScene
---@param sceneName string
function SceneManagerProxy:showScene(sceneName)
    self.maxIndex = self.curIndex + 1
    self.history[self.maxIndex] = sceneName
    self:serCurIndex(self.maxIndex)
end

function SceneManagerProxy:redo()
    if self.maxIndex > self.curIndex then
        self:serCurIndex(self.curIndex + 1)
    end
end

function SceneManagerProxy:undo()
    if self.curIndex > 1 then
        self:serCurIndex(self.curIndex - 1)
    end
end

---serCurIndex
---@param index number
function SceneManagerProxy:serCurIndex(index)
    self.curIndex = index
    self:sendNotification(NotificationName.SHOW_SCENE, self.history[index])
end

return SceneManagerProxy