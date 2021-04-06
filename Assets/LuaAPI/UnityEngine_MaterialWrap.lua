---@class UnityEngine.Material : UnityEngine.Object
---@field shader UnityEngine.Shader
---@field color UnityEngine.Color
---@field mainTexture UnityEngine.Texture
---@field mainTextureOffset UnityEngine.Vector2
---@field mainTextureScale UnityEngine.Vector2
---@field renderQueue int
---@field globalIlluminationFlags UnityEngine.MaterialGlobalIlluminationFlags
---@field doubleSidedGI bool
---@field enableInstancing bool
---@field passCount int
---@field shaderKeywords table
local m = {}
---@overload fun(name:string):bool
---@param nameID int
---@return bool
function m:HasProperty(nameID) end
---@param keyword string
function m:EnableKeyword(keyword) end
---@param keyword string
function m:DisableKeyword(keyword) end
---@param keyword string
---@return bool
function m:IsKeywordEnabled(keyword) end
---@param passName string
---@param enabled bool
function m:SetShaderPassEnabled(passName, enabled) end
---@param passName string
---@return bool
function m:GetShaderPassEnabled(passName) end
---@param pass int
---@return string
function m:GetPassName(pass) end
---@param passName string
---@return int
function m:FindPass(passName) end
---@param tag string
---@param val string
function m:SetOverrideTag(tag, val) end
---@overload fun(tag:string, searchFallbacks:bool):string
---@param tag string
---@param searchFallbacks bool
---@param defaultValue string
---@return string
function m:GetTag(tag, searchFallbacks, defaultValue) end
---@param start UnityEngine.Material
---@param end UnityEngine.Material
---@param t float
function m:Lerp(start, end, t) end
---@param pass int
---@return bool
function m:SetPass(pass) end
---@param mat UnityEngine.Material
function m:CopyPropertiesFromMaterial(mat) end
---@overload fun(outNames:table):void
---@return table
function m:GetTexturePropertyNames() end
---@overload fun(outNames:table):void
---@return table
function m:GetTexturePropertyNameIDs() end
---@overload fun(nameID:int, value:float):void
---@param name string
---@param value float
function m:SetFloat(name, value) end
---@overload fun(nameID:int, value:int):void
---@param name string
---@param value int
function m:SetInt(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Color):void
---@param name string
---@param value UnityEngine.Color
function m:SetColor(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Vector4):void
---@param name string
---@param value UnityEngine.Vector4
function m:SetVector(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Matrix4x4):void
---@param name string
---@param value UnityEngine.Matrix4x4
function m:SetMatrix(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Texture):void
---@param name string
---@param value UnityEngine.Texture
function m:SetTexture(name, value) end
---@overload fun(nameID:int, value:UnityEngine.ComputeBuffer):void
---@param name string
---@param value UnityEngine.ComputeBuffer
function m:SetBuffer(name, value) end
---@overload fun(nameID:int, values:table):void
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@param values table
function m:SetFloatArray(name, values) end
---@overload fun(nameID:int, values:table):void
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@param values table
function m:SetColorArray(name, values) end
---@overload fun(nameID:int, values:table):void
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@param values table
function m:SetVectorArray(name, values) end
---@overload fun(nameID:int, values:table):void
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@param values table
function m:SetMatrixArray(name, values) end
---@overload fun(nameID:int):float
---@param name string
---@return float
function m:GetFloat(name) end
---@overload fun(nameID:int):int
---@param name string
---@return int
function m:GetInt(name) end
---@overload fun(nameID:int):UnityEngine.Color
---@param name string
---@return UnityEngine.Color
function m:GetColor(name) end
---@overload fun(nameID:int):UnityEngine.Vector4
---@param name string
---@return UnityEngine.Vector4
function m:GetVector(name) end
---@overload fun(nameID:int):UnityEngine.Matrix4x4
---@param name string
---@return UnityEngine.Matrix4x4
function m:GetMatrix(name) end
---@overload fun(nameID:int):UnityEngine.Texture
---@param name string
---@return UnityEngine.Texture
function m:GetTexture(name) end
---@overload fun(nameID:int):table
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@return table
function m:GetFloatArray(name) end
---@overload fun(nameID:int):table
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@return table
function m:GetColorArray(name) end
---@overload fun(nameID:int):table
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@return table
function m:GetVectorArray(name) end
---@overload fun(nameID:int):table
---@overload fun(name:string, values:table):void
---@overload fun(nameID:int, values:table):void
---@param name string
---@return table
function m:GetMatrixArray(name) end
---@overload fun(nameID:int, value:UnityEngine.Vector2):void
---@param name string
---@param value UnityEngine.Vector2
function m:SetTextureOffset(name, value) end
---@overload fun(nameID:int, value:UnityEngine.Vector2):void
---@param name string
---@param value UnityEngine.Vector2
function m:SetTextureScale(name, value) end
---@overload fun(nameID:int):UnityEngine.Vector2
---@param name string
---@return UnityEngine.Vector2
function m:GetTextureOffset(name) end
---@overload fun(nameID:int):UnityEngine.Vector2
---@param name string
---@return UnityEngine.Vector2
function m:GetTextureScale(name) end
UnityEngine = {}
UnityEngine.Material = m
return m