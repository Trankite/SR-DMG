# SR-DMG

星穹铁道伤害计算器 | Star Rail Damage Calculator

## 功能

**伤害计算**：直伤 / 击破伤害 / 超击破伤害 / 持续伤害

**添加BUFF**：固定数值（ [攻击力]:1100 ），或是表达式（ [击破特攻]:([攻击力]-1800)*0.08% ）

**数据导入**：登录米哈游账号以导入所有角色，或任意UID的角色展柜

## 指令

**数据路径**：打开程序保存数据的路径（ *C:\Users\User\AppData\Local\SR-DMG* ）

    path

**登录米哈游账号**：弹出扫码登录界面，并保存账号的Cookie信息

*此信息位于：C:\Users\User\AppData\Local\SR-DMG\Token.json*

*声明：本程序不会收集你的信息，该数据仅会保存在你的设备中，不要将该文本共享给任何人*

*注意：若你进行登录，一切可能的后果自行承担*

    login
    
**导入数据**：所有角色的数据（ *需要先登录米哈游账号，仅该账号的角色* ），任何人的角色展柜（ *使用 Mihomo API* ）

*优先使用本地的数据（ C:\Users\User\AppData\Local\SR-DMG\Data\\{UID}.json ）*

*若本地没有该UID的数据，才会从云端重新导入*

    uid me              // 当前登录的米哈游账号的所有角色
    uid 100000000       // 角色展柜，数字替换为你的UID
    UID me              // 立即更新本地的数据
    UID 100000000       // 同上
    
**实时便筏**：查询当前开拓力，需要先登录米哈游账号

    note

**每日签到**：星穹铁道每日签到，需要先登录米哈游账号

    sign

**米游币任务**：完成米游币任务，需要先登录米哈游账号

    coin

## 提示

**基础数值**：右键点击 “攻击力/生命值/防御力/速度” 可以切换为 “基础攻击力/...”

**其他属性**：右键点击 “效果命中/效果抵抗” 可以切换为 “充能效率/治疗提高”

**连续操作**：右键点击 “快捷取参数” 及 计算器相关 的勾选框，可以锁定/解锁勾选

**界面透明**：空白区域长按右键，界面会进入透明状态，松开右键恢复

**取消保存**：右键点击 “等待保存” 会弹出取消保存计划的确认框

**执行指令**：右键点击 “指令” 可以立即执行，在文本框内按下 Enter 也是同样的效果

**伤害比较**：勾选 “记录此伤害” 后，显示伤害提升的百分比（若 0% 则不显示）

**切换组**：按下左下角的 “G” 按钮，会载入所有组的列表，选中以切换

**创建组**：当你未载入组时，此时的 “保存” 按钮 就是创建组

**删除组**：当你选中数据列表的第一项，此时会弹出 “删除组” 的确认框

**初始值**：当你选中文本框，文本框会显示移除 “增益/转化” 后的初始值，便于修改

## 安装

最新版本：[Release](https://github.com/Trankite/SR-DMG/releases)

*无法运行？尝试安装：[.NET 9.0](https://dotnet.microsoft.com/zh-cn/download)*
