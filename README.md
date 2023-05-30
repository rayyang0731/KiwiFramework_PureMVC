# KiwiFramework_PureMVC
Unity 使用的 PureMVC Framework

**Facade :**  单例模式, 负责初始化核心层( Model, Controller, View), 可以访问核心层的 Public 方法.保存 Command 与 Notification 之间的映射. 当 Notification 发出时, 对应的 Command 就会自动由 Controller 执行.

**Model :** 数据层, 保存 Proxy 对象的引用. 如果 View 层操作导致数据变化, 可以发送 Command 到 Model, Model 再调用 Proxy 中的 Set 方法, 从而达成修改数据的目的.

    **Proxy :** 负责操作数据模型, 与远端服务通信存取数据, 操作本地配置表数据, 可以发送 Notification, 但是不接收 Notification.

**View :** 保存 Mediator 对象的引用

    **Mediator :** 操作视图组件, 添加事件监听器,发送接收 Notification, 修改视图组件状态, 当 View 注册 Mediator 时, 注册 Mediator 对象所关心的所有 Notification.

**Controller :** 保存所有 Command 映射, APP 的业务逻辑应该在这里实现, 例如 : APP 的启动和关闭

    **Command :** 可以获得 Proxy 对象并交互, 发送 Notification 执行其他的 Command

**Observer 和 Notification :** 框架内部实现的消息机制.
