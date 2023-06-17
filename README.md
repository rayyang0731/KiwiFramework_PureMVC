# KiwiFramework_PureMVC

在 Unity 使用的 PureMVC Framework, 代码来自于 [puremvc-csharp-multicore-framework](https://github.com/PureMVC/puremvc-csharp-multicore-framework). 将代码中的注释翻译为中文, 对极小部分代码进行了修改.

---

### **Controller**

在 PureMVC 框架中, Controller 类遵循 “ 命令和控制器” 策略, 并承担以下责任:

- 保存所有 ICommand(命令) 的映射, 记录哪些 ICommand(命令) 用于处理哪些 INotifications(广播).
- 在 View 中, 将 ICommand(命令) 映射的 INotification(广播) 注册为 IObserver(观察者)
- 当 View 收到通知时, 创建一个适当的 ICommand(命令)的新实例来处理发送过来的 INotification(广播).
- 调用 `ICommand.Executde(notifiation)` 方法, 传递 INotification(广播)

> Command 类只在需要时才被创建, 并且可以有以下行为 :
>
> - 获取 Proxy 对象并与之交互
> - 发送 Notification
> - 执行其他 Command

---

### Model

在 PureMVC 框架中, Model 类保存对 Proxy 对象的引用, 通过名称查找 Proxy(代理) 并对其访问, 如果有外部行为需要修改数据, 则可以发送 Command(命令) , 由 Command 获取 Proxy 与之交互, Model 类主要承担以下责任:

- 维护 IProxy(代理) 实例的缓存
- 提供注册 | 检索 | 删除 IProxy 实例的方法

> Proxy 类负责操作数据模型,与远程服务通信存取数据, 读取本地配置表数据. Proxy 可以发送 Notification(通知), 但是不要监听或接收 Notification(通知).

---

### View

在 PureMVC 框架中, View 保存对 Mediator 对象的引用, View 类主要承担以下责任:

- 维护一个 IMediator(中介器) 实例的缓存
- 提供注册 | 检索 | 删除 IMediator(中介器) 的方法
- 管理应用程序中每个 INotification(通知) 的观察者列表
- 提供将 IObservers(观察者) 附加到 INotification(通知) 观察者列表的方法
- 提供广播 INotification(通知) 的方法
- 在广播时, 通知给定 INotification(通知) 的 IObservers(观察者)

> Mediator 类主要用来操作具体的视图组件,包括 : 添加事件监听, 发送或接收 Notification, 直接改变视图组件的状态.

---

### Facade

在 PureMVC 框架中, 作为外观层, Facade 负责初始化核心层( **Model**, **Controller**, **View** ), 可以访问核心层的 Public 方法, 同时支持 Proxy , Mediator 和 Command 通过创建的 Facade 类来相互访问通信.