---@class Framework.core.AssetsUtil
AssetsUtil = class("Framework.core.AssetsUtil")

function AssetsUtil.LoadPrefab(path)
    CSBridge.LoadPrefab(path)
end