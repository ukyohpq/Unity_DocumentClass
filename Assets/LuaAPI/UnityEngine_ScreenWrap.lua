---@class UnityEngine.Screen : object
---@field width int
---@field height int
---@field dpi float
---@field currentResolution UnityEngine.Resolution
---@field resolutions table
---@field fullScreen bool
---@field fullScreenMode UnityEngine.FullScreenMode
---@field safeArea UnityEngine.Rect
---@field cutouts table
---@field autorotateToPortrait bool
---@field autorotateToPortraitUpsideDown bool
---@field autorotateToLandscapeLeft bool
---@field autorotateToLandscapeRight bool
---@field orientation UnityEngine.ScreenOrientation
---@field sleepTimeout int
---@field brightness float
local m = {}
---@overload fun(width:int, height:int, fullscreenMode:UnityEngine.FullScreenMode):void
---@overload fun(width:int, height:int, fullscreen:bool, preferredRefreshRate:int):void
---@overload fun(width:int, height:int, fullscreen:bool):void
---@param width int
---@param height int
---@param fullscreenMode UnityEngine.FullScreenMode
---@param preferredRefreshRate int
function m.SetResolution(width, height, fullscreenMode, preferredRefreshRate) end
UnityEngine = {}
UnityEngine.Screen = m
return m