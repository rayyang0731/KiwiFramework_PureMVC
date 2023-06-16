namespace KiwiFramework.PureMVC.Interfaces
{
	/// <summary> 
	/// PureMVC 视图的接口定义。 
	/// </summary> 
	/// <remarks> 
	///     <para>在 PureMVC 中，<c>View</c> 类承担以下职责：</para> 
	///     <list type="bullet"> 
	///         <item>维护一个 <c>IMediator</c> 实例的缓存</item> 
	///         <item>提供注册、检索和删除 <c>IMediators</c> 的方法</item> 
	///         <item>管理应用程序中每个 <c>INotification</c> 的观察者列表</item> 
	///         <item>提供将 <c>IObservers</c> 附加到 <c>INotification</c> 的观察者列表的方法</item> 
	///         <item>提供广播 <c>INotification</c> 的方法</item> 
	///         <item>在广播时通知给定 <c>INotification</c> 的 <c>IObservers</c></item> 
	///     </list> 
	/// </remarks> 
	/// <seealso cref="IMediator"/> 
	/// <seealso cref="IObserver"/> 
	/// <seealso cref="INotification"/> 
	public interface IView
	{
		/// <summary> 
		/// 注册一个 <c>IObserver</c>，以便在给定名称的 <c>INotifications</c> 中得到通知。 
		/// </summary> 
		/// <param name="notificationName">通知此<c>IObserver</c>的<c>INotifications</c>的名称</param> 
		/// <param name="observer">要注册的<c>IObserver</c></param> 
		void RegisterObserver(string notificationName, IObserver observer);

		/// <summary> 
		/// 从给定通知名称的观察者列表中删除一组观察者。 
		/// </summary> 
		/// <param name="notificationName">要从中删除的观察者列表</param> 
		/// <param name="notifyContext">删除具有此对象作为其 notifyContext 的观察者</param> 
		void RemoveObserver(string notificationName, object notifyContext);

		/// <summary> 
		/// 通知特定 <c>INotification</c> 的 <c>IObservers</c>。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         此<c>INotification</c>列表中所有先前附加的<c>IObservers</c>都会按照它们注册的顺序收到通知，并传递一个对<c>INotification</c>的引用。 
		///     </para> 
		/// </remarks> 
		/// <param name="notification">要通知<c>IObservers</c>的<c>INotification</c>。</param> 
		void NotifyObservers(INotification notification);

		/// <summary> 
		/// 使用 <c>View</c> 注册一个 <c>IMediator</c> 实例。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         注册 <c>IMediator</c>，以便可以通过名称检索它，并进一步询问 <c>IMediator</c> 以获取其 
		///         <c>INotification</c> 兴趣。 
		///     </para> 
		///     <para> 
		///         如果 <c>IMediator</c> 返回任何要通知的 <c>INotification</c> 名称， 
		///         则创建一个封装 <c>IMediator</c> 实例的 <c>handleNotification</c> 方法的 <c>Observer</c>， 
		///         并将其注册为 <c>IMediator</c> 感兴趣的所有 <c>INotifications</c> 的 <c>Observer</c>。 
		///     </para> 
		/// </remarks> 
		/// <param name="mediator">对<c>IMediator</c>实例的引用</param> 
		void RegisterMediator(IMediator mediator);

		/// <summary> 
		/// 从 <c>View</c> 中检索一个 <c>IMediator</c>。 
		/// </summary> 
		/// <param name="mediatorName">要检索的 <c>IMediator</c> 实例的名称。</param> 
		/// <returns>先前使用给定的 <c>mediatorName</c> 注册的 <c>IMediator</c> 实例。</returns> 
		IMediator GetMediator(string mediatorName);

		/// <summary> 
		/// 从 <c>View</c> 中删除一个 <c>IMediator</c>。 
		/// </summary> 
		/// <param name="mediatorName">要删除的 <c>IMediator</c> 实例的名称。</param> 
		/// <returns>从 <c>View</c> 中删除的 <c>IMediator</c></returns> 
		IMediator RemoveMediator(string mediatorName);

		/// <summary> 
		/// 检查是否注册了 Mediator 
		/// </summary> 
		/// <param name="mediatorName"></param> 
		/// <returns>是否使用给定的 <c>mediatorName</c> 注册了 Mediator。</returns> 
		bool HasMediator(string mediatorName);
	}
}
