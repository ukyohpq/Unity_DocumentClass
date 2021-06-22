﻿---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by Administrator.
--- DateTime: 2021/1/19 11:31
---

--- prefab运行流程
--- 1.new一个prefab
--- 2.prefab通过cs接口加载资源
--- 3.加载完成之后，gameobject被放置到stage上指定的ui路径下，gameobject关联的DocumentClass链接Prefab(cs链接lua)，DocumentClass call Prefab:Init()
--- 4.Prefab在析构的时候，需要通知DocumentClass，
--- 5.gameobject在移除的时候，需要通过DocumentClass去调用Prefab:DestroyFromCS()


local super = require("Framework.display.DisplayObjectContainer")
---@class Framework.UI.Prefab:Framework.display.DisplayObjectContainer
---@field private status number
Prefab = class("Framework.UI.Prefab", super)

function Prefab:ctor(autoBind)
    super.ctor(self)
    self.status = 0
    self:AddEventListener(Event.INIT, self, self.OnInit)
    if autoBind ~= false then
        self:bindExtend()
    end
    --self:LoadResource()
end

function Prefab:start()
    self:StartLogic()
end

---OnComplete
---@param evt Framework.event.Event
function Prefab:OnInit(evt)
    self.status = 2
    LogUtil.LogError("OnInit:%s", self:GetName())
end

function Prefab:StartLogic()
    error("must override!")
end

function Prefab:GetAssetPath()
    error("must override!")
end

return Prefab