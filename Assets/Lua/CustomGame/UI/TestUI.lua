local super = PrefabLua

---@class CustomGame.UI.TestUI
---@field m_Text
---@field m_Button
TestUI = class("CustomGame.UI.TestUI", super)

function TestUI:ctor()
    super.ctor(self)
    self.m_Text = aa
    self.m_Button = aa
end

function TestUI:LoadResource()
    
end