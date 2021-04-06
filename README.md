## unity_documentclass
一、实现基于unity的文档类功能
基于tolua和emmylua，一种类似as3文档类的实现。
对一个prefab添加脚本组件:DocumentClass，并指定其类名，有两种方式可以将prefab和lua类联系起来：
1.代码创建即可在lua中通过实例化该类，得到该prefab的一个实例。
2.直接拖放prefab到hierarchy上，会自动创建一个对应的lua类型的实例，且该实例和prefab相关联。
不管实例是通过上述两种方法中的哪一种创建的，其符合命名规则的关键组件也会导出成为该类的的字段。
实例中的TestContainer类，可以
local prefab = TestContainer.New()
即可创建出TestContainer.prefab的实例，
prefab.m_Doc是一个TestUI的实例，
prefab.m_Doc.m_Text即为TestUI上的同名的文本组件
如下代码即可为文本赋值，并且给按钮注册click事件侦听器
prefab.m_Doc.m_Text.text = "这是文本1"
prefab.m_Doc.m2_Text.text = "这是文本2"
prefab.m_Doc.m_Button:AddEventListener("click", self, self.onClick)

二、lua运行时热更
在editor模式下运行工程，可以在不关闭工程的情况下，直接修改lua代码，且能够直接起效。
function XXX:onClick()
    ---@type CustomGame.UI.TestContainer
    local prefab = self.viewComponent
    self.numClick = self.numClick + 1
    --prefab.m_Doc.m2_Text.text = "按钮被点击了" .. self.numClick .. "次"
end
比如将这个事件侦听器，如果在运行时将最后一行的注释去掉，点击按钮后，会发现文本发生了变化，现在运行的是修改之后的代码。

三、整合了puremvc框架

四、范例
