---@class UnityEngine.Shader : UnityEngine.Object
---@field maximumLOD int
---@field globalMaximumLOD int
---@field isSupported bool
---@field globalRenderPipeline string
---@field renderQueue int
---@field passCount int
local m = {}
---@param name string
---@return UnityEngine.Shader
function m.Find(name) end
---@param keyword string
function m.EnableKeyword(keyword) end
---@param keyword string
function m.DisableKeyword(keyword) end
---@param keyword string
---@return bool
function m.IsKeywordEnabled(keyword) end
function m.WarmupAllShaders() end
---@param name string
---@return int
function m.PropertyToID(name) end
---@param name string
---@return UnityEngine.Shader
function m:GetDependency(name) end
---@param passIndex int
---@param tagName UnityEngine.Rendering.ShaderTagId
---@return UnityEngine.Rendering.ShaderTagId
function m:FindPassTagValue(passIndex, tagName) end
---@overload fun(nameID:int, value:float):void
---@param name string
---@param value float
function m.SetGlobalFloat(name, value) end
---@overload fun(nameID:int, value:int):void
---@param name string
---@param value int
function m.SetGlobalInt(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Vector4):void
---@param name string
---@param value UnityEngine.Vector4
function m.SetGlobalVector(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Color):void
---@param name string
---@param value UnityEngine.Color
function m.SetGlobalColor(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Matrix4x4):void
---@param name string
---@param value UnityEngine.Matrix4x4
function m.SetGlobalMatrix(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Texture):void
---@overload fun(name:string, value:UnityEngine.RenderTexture, element:UnityEngine.Rendering.RenderTextureSubElement):void
---@overload fun(nameID:int, value:UnityEngine.RenderTexture, element:UnityEngine.Rendering.RenderTextureSubElement):void
---@param name string
---@param value UnityEngine.Texture
function m.SetGlobalTexture(name, value) end
---@overload fun(nameID:int, value:UnityEngine.ComputeBuffer):void
---@overload fun(name:string, value:UnityEngine.GraphicsBuffer):void
---@overload fun(nameID:int, value:UnityEngine.GraphicsBuffer):void
---@param name string
---@param value UnityEngine.ComputeBuffer
function m.SetGlobalBuffer(name, value) end
---@overload fun(nameID:int, value:UnityEngine.ComputeBuffer, offset:int, size:int):void
---@overload fun(name:string, value:UnityEngine.GraphicsBuffer, offset:int, size:int):void
---@overload fun(nameID:int, value:UnityEngine.GraphicsBuffer, offset:int, size:int):void
---@param name string
---@param value UnityEngine.ComputeBuffer
---@param offset int
---@param size int
function m.SetGlobalConstantBuffer(name, value, offset, size) end
---@overload fun(nameID:int, values:table):void
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@param values table
function m.SetGlobalFloatArray(name, values) end
---@overload fun(nameID:int, values:table):void
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@param values table
function m.SetGlobalVectorArray(name, values) end
---@overload fun(nameID:int, values:table):void
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@param values table
function m.SetGlobalMatrixArray(name, values) end
---@overload fun(nameID:int):float
---@param name string
---@return float
function m.GetGlobalFloat(name) end
---@overload fun(nameID:int):int
---@param name string
---@return int
function m.GetGlobalInt(name) end
---@overload fun(nameID:int):UnityEngine.Vector4
---@param name string
---@return UnityEngine.Vector4
function m.GetGlobalVector(name) end
---@overload fun(nameID:int):UnityEngine.Color
---@param name string
---@return UnityEngine.Color
function m.GetGlobalColor(name) end
---@overload fun(nameID:int):UnityEngine.Matrix4x4
---@param name string
---@return UnityEngine.Matrix4x4
function m.GetGlobalMatrix(name) end
---@overload fun(nameID:int):UnityEngine.Texture
---@param name string
---@return UnityEngine.Texture
function m.GetGlobalTexture(name) end
---@overload fun(nameID:int):table
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@return table
function m.GetGlobalFloatArray(name) end
---@overload fun(nameID:int):table
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@return table
function m.GetGlobalVectorArray(name) end
---@overload fun(nameID:int):table
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@return table
function m.GetGlobalMatrixArray(name) end
---@return int
function m:GetPropertyCount() end
---@param propertyName string
---@return int
function m:FindPropertyIndex(propertyName) end
---@param propertyIndex int
---@return string
function m:GetPropertyName(propertyIndex) end
---@param propertyIndex int
---@return int
function m:GetPropertyNameId(propertyIndex) end
---@param propertyIndex int
---@return UnityEngine.Rendering.ShaderPropertyType
function m:GetPropertyType(propertyIndex) end
---@param propertyIndex int
---@return string
function m:GetPropertyDescription(propertyIndex) end
---@param propertyIndex int
---@return UnityEngine.Rendering.ShaderPropertyFlags
function m:GetPropertyFlags(propertyIndex) end
---@param propertyIndex int
---@return table
function m:GetPropertyAttributes(propertyIndex) end
---@param propertyIndex int
---@return float
function m:GetPropertyDefaultFloatValue(propertyIndex) end
---@param propertyIndex int
---@return UnityEngine.Vector4
function m:GetPropertyDefaultVectorValue(propertyIndex) end
---@param propertyIndex int
---@return UnityEngine.Vector2
function m:GetPropertyRangeLimits(propertyIndex) end
---@param propertyIndex int
---@return UnityEngine.Rendering.TextureDimension
function m:GetPropertyTextureDimension(propertyIndex) end
---@param propertyIndex int
---@return string
function m:GetPropertyTextureDefaultName(propertyIndex) end
---@param propertyIndex int
---@param stackName string
---@param layerIndex int
---@return bool
function m:FindTextureStack(propertyIndex, stackName, layerIndex) end
UnityEngine = {}
UnityEngine.Shader = m
return m