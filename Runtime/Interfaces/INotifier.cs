namespace KiwiFramework.PureMVC.Interfaces
{
    /// <summary> 
    /// 基本的<c>INotifier</c>实现。 
    /// </summary> 
    /// <remarks> 
    /// <para> 
    /// <c>MacroCommand，Command，Mediator</c>和<c>Proxy</c>都需要发送<c>Notifications</c>。 
    /// </para> 
    /// <para> 
    /// <c>INotifier</c>接口提供了一个通用方法<c>sendNotification</c>，它可以减轻实现代码构造<c>Notifications</c>的必要性。 
    /// </para> 
    /// <para> 
    /// 所有上述类都扩展了Notifier类，该类提供了对Facade Multiton的初始化引用，这是发送<c>Notifications</c>的便捷方法所必需的，但也简化了实现，因为这些类经常与<c>Facade</c>交互并且通常需要访问外观。 
    /// </para> 
    /// </remarks> 
    /// <seealso cref="IFacade"/> 
    /// <seealso cref="INotification"/> 
    public interface INotifier
    {
        /// <summary> 
        /// 发送<c>INotification</c>。 
        /// </summary> 
        /// <remarks> 
        /// <para> 
        /// 为了避免在实现代码中构造新的通知实例，提供了方便的方法。 
        /// </para> 
        /// </remarks> 
        /// <param name="notificationName">要发送的通知的名称</param> 
        /// <param name="body">通知的主体（可选）</param> 
        /// <param name="type">通知的类型（可选）</param> 
        void SendNotification(string notificationName, object body = null, string type = null);

        /// <summary> 
        /// 初始化此INotifier实例。 
        /// </summary> 
        /// <remarks> 
        /// <para> 
        /// 这是一个Notifier获取其multitonKey的方式。在调用此方法之前，对sendNotification或访问外观的调用将失败。 
        /// </para> 
        /// </remarks> 
        /// <param name="key">此INotifier要使用的multitonKey</param> 
        void InitializeNotifier(string key);
    }
}
