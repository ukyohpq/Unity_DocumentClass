﻿---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by Administrator.
--- DateTime: 2021/1/19 11:31
---

local prefabIDMap = {}
local function getPrefabID()
    
end

---@class PrefabLua
PrefabLua = class("PrefabLua")

function PrefabLua:ctor()
    
end

function PrefabLua:start()
    self:StartLogic()
end

function PrefabLua:LoadResource()
    local path = self:GetPrefabPath()
    
end

function PrefabLua:StartLogic()
    error("must override!")
end

function PrefabLua:GetPrefabPath()
    error("must override!")
end