---@class UnityEngine.Light : UnityEngine.Behaviour
---@field type UnityEngine.LightType
---@field spotAngle float
---@field color UnityEngine.Color
---@field colorTemperature float
---@field intensity float
---@field bounceIntensity float
---@field shadowCustomResolution int
---@field shadowBias float
---@field shadowNormalBias float
---@field shadowNearPlane float
---@field range float
---@field flare UnityEngine.Flare
---@field bakingOutput UnityEngine.LightBakingOutput
---@field cullingMask int
---@field lightShadowCasterMode UnityEngine.LightShadowCasterMode
---@field shadows UnityEngine.LightShadows
---@field shadowStrength float
---@field shadowResolution UnityEngine.Rendering.LightShadowResolution
---@field layerShadowCullDistances table
---@field cookieSize float
---@field cookie UnityEngine.Texture
---@field renderMode UnityEngine.LightRenderMode
---@field commandBufferCount int
local m = {}
function m:Reset() end
---@overload fun(evt:UnityEngine.Rendering.LightEvent, buffer:UnityEngine.Rendering.CommandBuffer, shadowPassMask:UnityEngine.Rendering.ShadowMapPass):void
---@param evt UnityEngine.Rendering.LightEvent
---@param buffer UnityEngine.Rendering.CommandBuffer
function m:AddCommandBuffer(evt, buffer) end
---@overload fun(evt:UnityEngine.Rendering.LightEvent, buffer:UnityEngine.Rendering.CommandBuffer, shadowPassMask:UnityEngine.Rendering.ShadowMapPass, queueType:UnityEngine.Rendering.ComputeQueueType):void
---@param evt UnityEngine.Rendering.LightEvent
---@param buffer UnityEngine.Rendering.CommandBuffer
---@param queueType UnityEngine.Rendering.ComputeQueueType
function m:AddCommandBufferAsync(evt, buffer, queueType) end
---@param evt UnityEngine.Rendering.LightEvent
---@param buffer UnityEngine.Rendering.CommandBuffer
function m:RemoveCommandBuffer(evt, buffer) end
---@param evt UnityEngine.Rendering.LightEvent
function m:RemoveCommandBuffers(evt) end
function m:RemoveAllCommandBuffers() end
---@param evt UnityEngine.Rendering.LightEvent
---@return table
function m:GetCommandBuffers(evt) end
---@param type UnityEngine.LightType
---@param layer int
---@return table
function m.GetLights(type, layer) end
UnityEngine = {}
UnityEngine.Light = m
return m