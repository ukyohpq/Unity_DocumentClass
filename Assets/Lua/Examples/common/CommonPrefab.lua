local super = require("Framework.UI.Prefab")

---@class Examples.common.CommonPrefab:Framework.UI.Prefab
CommonPrefab = class("Examples.common.CommonPrefab", Prefab)

function CommonPrefab:ctor(autobind)
    super.ctor(self, autobind)
end

return CommonPrefab