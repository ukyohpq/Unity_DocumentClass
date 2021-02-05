require("FrameWork.class")
require("FrameWork.event.Event")
require("FrameWork.event.EventDispatcher")

local a = {}
---fun
---@param evt Framework.event.Event
function a:fun(evt)
    print("a", unpack(evt:GetEventData()))
end
---funb
---@param evt Framework.event.Event
function a:funb(evt)
    print("b", unpack(evt:GetEventData()))
end

local dispatcher = EventDispatcher.New()
dispatcher:AddEventListener("xxx", a, a.fun)

dispatcher:DispatchMessage("xxx") ---
dispatcher:DispatchMessage("xxx",1) ---a 1
dispatcher:DispatchMessage("xxx",1, 2) ---a 1 2
print("test1")
dispatcher:RemoveEventListener("xxx", a, a.fun)

dispatcher:DispatchMessage("xxx") ---
dispatcher:DispatchMessage("xxx",1) ---
dispatcher:DispatchMessage("xxx",1, 2) ---
print("test2")

dispatcher:AddEventListener("xxx", a, a.fun)
dispatcher:AddEventListener("xxx", a, a.fun)

dispatcher:DispatchMessage("xxx") ---
dispatcher:DispatchMessage("xxx",1) ---a 1
dispatcher:DispatchMessage("xxx",1, 2) ---a 1 2
print("test3")

dispatcher:RemoveEventListener("xxx", a, a.fun)

dispatcher:DispatchMessage("xxx") ---
dispatcher:DispatchMessage("xxx",1) ---
dispatcher:DispatchMessage("xxx",1, 2) ---
print("test4")

dispatcher:AddEventListener("xxx", a, a.fun)
dispatcher:RemoveAllEventListeners()
dispatcher:DispatchMessage("xxx") ---
dispatcher:DispatchMessage("xxx",1) ---
dispatcher:DispatchMessage("xxx",1, 2) ---
print("test5")

dispatcher:AddEventListener("xxx", a, a.funb)
dispatcher:RemoveEventListener("bbb", a, a.fun)
dispatcher:AddEventListener("bbb", a, a.fun)
dispatcher:DispatchMessage("xxx") ---
dispatcher:DispatchMessage("xxx",1) ---
dispatcher:DispatchMessage("xxx",1, 2) ---
print("test6")