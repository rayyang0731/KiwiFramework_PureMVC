using System;

namespace KiwiFramework.PureMVC.Interfaces
{
    /// <summary>
    /// PureMVC Facade 的接口定义.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         外观模式建议, 提供一个类作为子系统通信的中心点.
    ///         在 PureMVC 中, Facade 充当核心 MVC , Model, View, Controller 与应用程序其余部分之间的接口.
    ///     </para>
    /// </remarks>
    /// <seealso cref="IModel"/>
    /// <seealso cref="IView"/>
    /// <seealso cref="IController"/>
    /// <seealso cref="ICommand"/>
    /// <seealso cref="INotification"/>
    public interface IFacade: INotifier
    {
        /// <summary>
        /// 向 <c>Model</c> 注册 <c>IProxy</c>。
        /// </summary>
        /// <param name="proxy">要向 <c>Model</c> 注册的 <c>IProxy</c> 实例.</param>
        void RegisterProxy(IProxy proxy);

        /// <summary>
        /// 按名称从 <c>Model</c> 中获取 <c>IProxy</c> 对象.
        /// </summary>
        /// <param name="proxyName">要获取的 <c>IProxy</c> 名称.</param>
        /// <returns>通过 <paramref name="proxyName"/> 获取到的 <c>IProxy</c> 实例.</returns>
        IProxy GetProxy(string proxyName);

        /// <summary>
        /// 根据 <c>proxyName</c> 从 <c>Model</c> 移除 <c>IProxy</c>
        /// </summary>
        /// <param name="proxyName">要移除的 <c>IProxy</c> 名称.</param>
        /// <returns>从 <c>Model</c> 中移除的 <c>IProxy</c>.</returns>
        IProxy RemoveProxy(string proxyName);

        /// <summary>
        /// 检测 <c>IProxy</c> 是否被注册
        /// </summary>
        /// <param name="proxyName">要检测的 <c>IProxy</c> 名称.</param>
        /// <returns>当前是否注册了 <param name="proxyName"/> 的 <c>IProxy</c>.</returns>
        bool HasProxy(string proxyName);

        /// <summary>
        /// 通过 Notification 的名称向 <c>Controller</c> 注册 <c>ICommand</c>。
        /// </summary>
        /// <param name="notificationName">与 <c>ICommand</c> 关联的 <c>INotification</c> 的名称</param>
        /// <param name="commandFunc">对 <c>ICommand</c> 类的引用</param>
        void RegisterCommand(string notificationName, Func<ICommand> commandFunc);

        /// <summary>
        /// 通过 Notification 的名称从 <c>Controller</c> 中移除 <c>ICommand</c>.
        /// </summary>
        /// <param name="notificationName">与 <c>ICommand</c> 关联的 <c>INotification</c> 的名称</param>
        void RemoveCommand(string notificationName);

        /// <summary>
        /// 检查是否注册了对应 Notification 名称的 <c>ICommand</c>
        /// </summary>
        /// <param name="notificationName">与 <c>ICommand</c> 关联的 <c>INotification</c> 的名称</param>
        /// <returns>当前是否注册了 <param name="notificationName"/> 的 <c>ICommand</c>.</returns>
        bool HasCommand(string notificationName);

        /// <summary>
        /// 向 <c>View</c> 注册 <c>IMediator</c>
        /// </summary>
        /// <param name="mediator">要向 <c>View</c> 注册的 <c>IMediator</c> 实例.</param>
        void RegisterMediator(IMediator mediator);

        /// <summary>
        /// 从 <c>View</c> 中获取 <c>IMediator</c>
        /// </summary>
        /// <param name="mediatorName">要获取的 <c>IMediator</c> 名称.</param>
        /// <returns>通过 <paramref name="mediatorName"/> 获取到的 <c>IMediator</c> 实例.</returns>
        IMediator GetMediator(string mediatorName);

        /// <summary>
        /// 根据 <c>mediatorName</c> 从 <c>View</c> 移除 <c>IMediator</c>
        /// </summary>
        /// <param name="mediatorName">要移除的 <c>IMediator</c> 名称.</param>
        /// <returns>从 <c>View</c> 中移除的 <c>IMediator</c>.</returns>
        IMediator RemoveMediator(string mediatorName);

        /// <summary>
        /// 检测 <c>IMediator</c> 是否被注册
        /// </summary>
        /// <param name="mediatorName">要检测的 <c>IMediator</c> 名称.</param>
        /// <returns>当前是否注册了 <param name="mediatorName"/> 的 <c>IMediator</c>.</returns>
        bool HasMediator(string mediatorName);

        /// <summary>
        /// 通知 <c>Observer</c>.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         这个方法是公共方法, 主要是为了向后兼容, 并允许发送自定义通知类.
        ///         通常应该只调用 <c>SendNotification</c> 并传递参数,
        ///			而不必自己构建 <c>INotification</c>。
        ///     </para>
        /// </remarks>
        /// <param name="notification">要通知出去的 <c>IINotification</c> 实例.</param>
        void NotifyObservers(INotification notification);
    }
}
