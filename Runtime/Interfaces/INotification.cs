namespace KiwiFramework.PureMVC.Interfaces
{
    /// <summary> 
    /// 基础<c>INotification</c>实现。 
    /// </summary> 
    /// <remarks> 
    ///     <para> 
    ///         PureMVC不依赖于底层事件模型，例如Flash提供的事件模型，而ActionScript 3没有固有的事件模型。 
    ///     </para> 
    ///     <para> 
    ///         PureMVC中实现的观察者模式存在的目的是支持应用程序和MVC三元组的角色之间的事件驱动通信。 
    ///     </para> 
    ///     <para> 
    ///         通知不是Flex/Flash/Apollo中事件的替代品。通常，<c>IMediator</c>实现者在其视图组件上放置事件侦听器，然后以通常的方式处理它们。这可能会导致广播<c>Notification</c>来触发<c>ICommand</c>或与其他<c>IMediator</c>通信。<c>IProxy</c>和<c>ICommand</c>实例通过广播<c>INotification</c>相互通信，以及<c>IMediator</c>。 
    ///     </para> 
    ///     <para> 
    ///         Flash<c>Event</c>和PureMVC<c>Notification</c>之间的一个关键区别是，<c>Event</c>遵循“责任链”模式，在显示层次结构中“冒泡”，直到某个父组件处理<c>Event</c>，而PureMVC<c>Notification</c>遵循“发布/订阅”模式。 PureMVC类不必在父/子关系中彼此相关，以便使用<c>Notification</c>相互通信。 
    ///     </para> 
    /// </remarks> 
    /// <seealso cref="IView"/> 
    /// <seealso cref="IObserver"/> 
    public interface INotification
    {
        /// <summary> 
        /// 获取<c>INotification</c>实例的名称。没有setter，只能由构造函数设置。 
        /// </summary> 
        string Name { get; }

        /// <summary> 
        /// 获取或设置<c>INotification</c>实例的主体 
        /// </summary> 
        object Body { get; set; }

        /// <summary> 
        /// 获取或设置<c>INotification</c>实例的类型 
        /// </summary> 
        string Type { get; set; }

        /// <summary> 
        /// 获取<c>INotification</c>实例的字符串表示形式 
        /// </summary> 
        /// <returns>字符串表示形式</returns> 
        string ToString();
    }
}
