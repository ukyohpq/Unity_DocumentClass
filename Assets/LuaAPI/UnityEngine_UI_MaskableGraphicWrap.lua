---@class UnityEngine.UI.MaskableGraphic : UnityEngine.UI.Graphic
---@field onCullStateChanged UnityEngine.UI.MaskableGraphic.CullStateChangedEvent
---@field maskable bool
---@field isMaskingGraphic bool
local m = {}
---@param baseMaterial UnityEngine.Material
---@return UnityEngine.Material
function m:GetModifiedMaterial(baseMaterial) end
---@param clipRect UnityEngine.Rect
---@param validRect bool
function m:Cull(clipRect, validRect) end
---@param clipRect UnityEngine.Rect
---@param validRect bool
function m:SetClipRect(clipRect, validRect) end
---@param clipSoftness UnityEngine.Vector2
function m:SetClipSoftness(clipSoftness) end
function m:RecalculateClipping() end
function m:RecalculateMasking() end
UnityEngine = {}
UnityEngine.UI = {}
UnityEngine.UI.MaskableGraphic = m
return m