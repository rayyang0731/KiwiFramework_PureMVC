using System;

using KiwiFramework.PureMVC.Interfaces;
using KiwiFramework.PureMVC.Core;

using System.Collections.Concurrent;

namespace KiwiFramework.PureMVC.Patterns
{
	/// <summary>
	/// 多例 <c>IFacade</c> 实现
	/// </summary>
	/// <seealso cref="Model"/>
	/// <seealso cref="View"/>
	/// <seealso cref="Controller"/>
	public class Facade : IFacade
	{
		/// <summary>
		/// 构造方法.
		/// </summary>
		/// <param name="key">唯一键值</param>
		/// <remarks>
		/// 	<para>
		///			<c>IFacade</c> 的实现是一个多例模式，
		/// 		所以不应该直接调用构造函数，而是调用静态工厂方法，
		/// 		传递这个实例的唯一键值.
		/// 	</para>
		/// </remarks>
		/// <example>
		/// 	<code>
		/// 		Facade.GetInstance( multitonKey, key => new Facade(key) )
		/// 	</code>
		/// </example>
		public Facade(string key)
		{
			InitializeNotifier(key);
			InstanceMap.TryAdd(key, new Lazy<IFacade>(() => this));
			InitializeFacade();
		}

		/// <summary>
		/// Facade 多例的工厂方法
		/// </summary>
		/// <param name="key">Facade 的唯一键值</param>
		/// <param name="factory">工厂的创建方法</param>
		/// <returns>Facade 的实例</returns>
		public static IFacade GetInstance(string key, Func<string, IFacade> factory)
			=> InstanceMap.GetOrAdd(key, new Lazy<IFacade>(() => factory(key))).Value;

		/// <summary>
		/// 初始化多例 <c>Facade</c> 实例。
		/// </summary>
		/// <remarks>
		/// 	<para>
		/// 		由构造函数自动调用, 可以在子类中重写, 以初始化子类自定义的内容.
		/// 		但是 override 方法中, 一定要调用 <c>base.InitializeFacade()</c> 方法
		/// 	</para>
		/// </remarks>
		protected virtual void InitializeFacade()
		{
			InitializeModel();
			InitializeController();
			InitializeView();
		}

		/// <summary>
		/// 初始化 <c>Controller</c>.
		/// </summary>
		/// <remarks>
		///     <para>
		///			由 <c>InitializeFacade</c> 方法调用.
		///			如果有以下需求，可以在 <c>Facade</c> 的子类中覆写此方法 : 
		///         <list type="bullet">
		///             <item>初始化一个不同的 <c>IController</c></item>
		///             <item>有 <c>Commands</c> 需要在启动时向 <c>Controller</c> 注册</item>
		///         </list>
		///			如果不想初始化不同的 <c>IController</c>,
		///			请在方法的开头调用 <c>base.InitializeController()</c>，
		///			然后再注册 <c>Command</c>.
		///     </para>
		/// </remarks>
		protected virtual void InitializeController()
			=> controller = Controller.GetInstance(multitonKey, key => new Controller(key));

		/// <summary>
		/// 初始化 <c>Model</c>.
		/// </summary>
		/// <remarks>
		///     <para>
		///			由 <c>InitializeFacade</c> 方法调用.
		///			如果有以下需求，可以在 <c>Facade</c> 的子类中覆写此方法 : 
		///			<list type="bullet">
		///             <item>初始化一个不同的 <c>IModel</c>.</item>
		///             <item>在构造时有 <c>Proxy</c> 需要注册到 <c>Model</c>.</item>
		///         </list>
		///			如果不想初始化不同的 <c>IModel</c>,
		///			请在方法的开头调用 <c>base.InitializeModel()</c>，
		///			然后再注册 <c>Command</c>.
		///     </para>
		///     <para>
		///			注意:
		///			此方法<i>很少</i>会需要覆写.
		///			实际上，更有可能使用 <c>Command</c> 来创建 <c>Proxy</c> 并将其注册到 <c>Model</c>，
		///			因为 <c>Proxy</c> 具有可变数据,可能需要发送 <c>INotification</c>，
		///			因此可能希望在构造过程中获取对 <c>Facade</c> 的引用。
		///     </para>
		/// </remarks>
		protected virtual void InitializeModel()
			=> model = Model.GetInstance(multitonKey, key => new Model(key));

		/// <summary>
		/// 初始化 <c>View</c>.
		/// </summary>
		/// <remarks>
		///     <para>
		///			由 <c>InitializeFacade</c> 方法调用.
		///			如果有以下需求，可以在 <c>Facade</c> 的子类中覆写此方法 : 
		///			<list type="bullet">
		///             <item>初始化一个不同的 <c>IView</c>.</item>
		///             <item>在构造时有 <c>Observers</c> 需要注册到 <c>View</c>.</item>
		///         </list>
		///     </para>
		/// </remarks>
		protected virtual void InitializeView()
			=> view = View.GetInstance(multitonKey, key => new View(key));

		/// <summary>
		/// 通过 Notification 的名称向 <c>Controller</c> 注册 <c>ICommand</c>。
		/// </summary>
		/// <param name="notificationName">与 <c>ICommand</c> 关联的 <c>INotification</c> 的名称</param>
		/// <param name="commandFunc">对 <c>ICommand</c> 类的引用</param>
		public virtual void RegisterCommand(string notificationName, Func<ICommand> commandFunc)
			=> controller.RegisterCommand(notificationName, commandFunc);

		/// <summary>
		/// 通过 Notification 的名称从 <c>Controller</c> 中移除 <c>ICommand</c>.
		/// </summary>
		/// <param name="notificationName">与 <c>ICommand</c> 关联的 <c>INotification</c> 的名称</param>
		public virtual void RemoveCommand(string notificationName)
			=> controller.RemoveCommand(notificationName);

		/// <summary>
		/// 检查是否注册了对应 Notification 名称的 <c>ICommand</c>
		/// </summary>
		/// <param name="notificationName">与 <c>ICommand</c> 关联的 <c>INotification</c> 的名称</param>
		/// <returns>当前是否注册了 <param name="notificationName"/> 的 <c>ICommand</c>.</returns>
		public virtual bool HasCommand(string notificationName)
			=> controller.HasCommand(notificationName);

		/// <summary>
		/// 向 <c>Model</c> 注册 <c>IProxy</c>。
		/// </summary>
		/// <param name="proxy">要向 <c>Model</c> 注册的 <c>IProxy</c> 实例.</param>
		public virtual void RegisterProxy(IProxy proxy)
			=> model.RegisterProxy(proxy);

		/// <summary>
		/// 按名称从 <c>Model</c> 中获取 <c>IProxy</c> 对象.
		/// </summary>
		/// <param name="proxyName">要获取的 <c>IProxy</c> 名称.</param>
		/// <returns>通过 <paramref name="proxyName"/> 获取到的 <c>IProxy</c> 实例.</returns>
		public virtual IProxy GetProxy(string proxyName)
			=> model.GetProxy(proxyName);

		/// <summary>
		/// 根据 <c>proxyName</c> 从 <c>Model</c> 移除 <c>IProxy</c>
		/// </summary>
		/// <param name="proxyName">要移除的 <c>IProxy</c> 名称.</param>
		/// <returns>从 <c>Model</c> 中移除的 <c>IProxy</c>.</returns>
		public virtual IProxy RemoveProxy(string proxyName)
			=> model.RemoveProxy(proxyName);

		/// <summary>
		/// 检测 <c>IProxy</c> 是否被注册
		/// </summary>
		/// <param name="proxyName">要检测的 <c>IProxy</c> 名称.</param>
		/// <returns>当前是否注册了 <param name="proxyName"/> 的 <c>IProxy</c>.</returns>
		public virtual bool HasProxy(string proxyName)
			=> model.HasProxy(proxyName);

		/// <summary>
		/// 向 <c>View</c> 注册 <c>IMediator</c>
		/// </summary>
		/// <param name="mediator">要向 <c>View</c> 注册的 <c>IMediator</c> 实例.</param>
		public virtual void RegisterMediator(IMediator mediator)
			=> view.RegisterMediator(mediator);

		/// <summary>
		/// 从 <c>View</c> 中获取 <c>IMediator</c>
		/// </summary>
		/// <param name="mediatorName">要获取的 <c>IMediator</c> 名称.</param>
		/// <returns>通过 <paramref name="mediatorName"/> 获取到的 <c>IMediator</c> 实例.</returns>
		public virtual IMediator GetMediator(string mediatorName)
			=> view.GetMediator(mediatorName);

		/// <summary>
		/// 根据 <c>mediatorName</c> 从 <c>View</c> 移除 <c>IMediator</c>
		/// </summary>
		/// <param name="mediatorName">要移除的 <c>IMediator</c> 名称.</param>
		/// <returns>从 <c>View</c> 中移除的 <c>IMediator</c>.</returns>
		public virtual IMediator RemoveMediator(string mediatorName)
			=> view.RemoveMediator(mediatorName);

		/// <summary>
		/// 检测 <c>IMediator</c> 是否被注册
		/// </summary>
		/// <param name="mediatorName">要检测的 <c>IMediator</c> 名称.</param>
		/// <returns>当前是否注册了 <param name="mediatorName"/> 的 <c>IMediator</c>.</returns>
		public virtual bool HasMediator(string mediatorName)
			=> view.HasMediator(mediatorName);

		/// <summary>
		/// 发送 <c>INotification</c>.
		/// </summary>
		/// <remarks>
		///     <para>
		///			这样可以不必在实现代码中构造新的通知实例
		///     </para>
		/// </remarks>
		/// <param name="notificationName">要发送的通知名称</param>
		/// <param name="body">通知数据 (optional)</param>
		/// <param name="type">通知类型 (optional)</param>
		public virtual void SendNotification(string notificationName, object body = null, string type = null)
			=> NotifyObservers(new Notification(notificationName, body, type));
		
		

		/// <summary>
		/// 通知 <c>Observer</c>.
		/// </summary>
		/// <remarks>
		///     <para>
		///			这个方法是公共方法, 主要是为了向后兼容, 并允许发送自定义通知类.
		///         通常应该只调用 <see cref="SendNotification"/> 并传递参数,
		///			而不必自己构建 <c>INotification</c>。
		///     </para>
		/// </remarks>
		/// <param name="notification">要通知出去的 <c>IINotification</c> 实例.</param>
		public virtual void NotifyObservers(INotification notification)
			=> view.NotifyObservers(notification);

		/// <summary>
		/// 为 <c>Facade</c> 实例设置多例唯一键值
		/// </summary>
		/// <remarks>
		///     <para>
		///			不直接调用, 而是在调用 <see cref="GetInstance"/> 时, 从构造函数调用.
		///			必须定义为公共方法, 才能实现 <c>INotifier</c>。
		///     </para>
		/// </remarks>
		/// <param name="key"><c>Facade</c> 多例唯一键值</param>
		public virtual void InitializeNotifier(string key) { multitonKey = key; }

		/// <summary>
		/// 检测指定多例唯一键值的 <c>Facade</c> 是否被注册过.
		/// </summary>
		/// <param name="key">要检测的多例唯一键值</param>
		/// <returns>是否已使用给定的 <c>key</c> 注册了 <c>Facade</c>.</returns>
		public static bool HasCore(string key)
			=> InstanceMap.TryGetValue(key, out _);

		/// <summary>
		/// 移除一个指定多例唯一键值的 <c>Facade</c>
		/// </summary>
		/// <remarks>
		///     <para>
		///         删除给定键值的 <c>Model</c>, <c>View</c>, <c>Controller</c> 和 <c>Facade</c> 的实例。
		///     </para>
		/// </remarks>
		/// <param name="key">要移除的 <c>Facade</c> 的多例唯一键值</param>
		public static void RemoveCore(string key)
		{
			if (InstanceMap.TryGetValue(key, out _) == false) return;

			Model.RemoveModel(key);
			View.RemoveView(key);
			Controller.RemoveController(key);

			InstanceMap.TryRemove(key, out _);
		}

		/// <summary>
		/// 控制器层
		/// </summary>
		protected IController controller;

		/// <summary>
		/// 模型层
		/// </summary>
		protected IModel model;

		/// <summary>
		/// 视图层
		/// </summary>
		protected IView view;

		/// <summary>
		/// 多例唯一键值
		/// </summary>
		protected string multitonKey;

		/// <summary>
		/// 多例 <c>Facade</c> 全部实例的 Map
		/// </summary>
		protected static readonly ConcurrentDictionary<string, Lazy<IFacade>> InstanceMap = new();
	}
}