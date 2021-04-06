--TODO 处理这个require
require("CustomGame.UI.TestUI")

---@type Framework.UI.Prefab
local super = require("Framework.UI.Prefab")

---@class CustomGame.UI.TestContainer:Framework.UI.Prefab
---@field m_Doc CustomGame.UI.TestUI
TestContainer = class("CustomGame.UI.TestContainer", super)

function TestContainer:GetAssetPath()
    if IsEditor then
        return "Assets/prefabs/TestContainer.prefab"
    else
        return "2"
    end
end

return TestContainer
