namespace KiwiFramework.PureMVC.Interfaces
{
	/// <summary> 
	/// PureMVC中中介者的接口定义。 
	/// </summary> 
	/// <remarks> 
	///     <para> 
	///         在PureMVC中，<c>IMediator</c>的实现者承担以下责任： 
	///         <list type="bullet"> 
	///             <item>实现一个通用方法，返回<c>IMediator</c>感兴趣的所有<c>INotification</c>的列表。</item> 
	///             <item>实现一个通知回调方法。</item> 
	///             <item>实现在将IMediator从View中注册或删除时调用的方法。</item> 
	///         </list> 
	///     </para> 
	///     <para> 
	///         此外，<c>IMediator</c>通常： 
	///         <list type="bullet"> 
	///             <item>充当一个或多个视图组件（如文本框或列表控件）之间的中介，维护引用并协调它们的行为。</item> 
	///             <item>在基于Flash的应用程序中，这通常是添加事件侦听器的地方，以及实现它们的处理程序。</item> 
	///             <item>响应和生成<c>INotifications</c>，与PureMVC应用程序的其他部分交互。</item> 
	///         </list> 
	///     </para> 
	///     <para> 
	///         当一个<c>IMediator</c>在<c>IView</c>中注册时， 
	///         <c>IView</c>将调用<c>IMediator</c>的<c>listNotificationInterests</c>方法。 
	///         <c>IMediator</c>将返回一个<c>INotification</c>名称的<c>Array</c>，它希望被通知。 
	///     </para> 
	///     <para> 
	///         然后，<c>IView</c>将创建一个封装了该<c>IMediator</c>的(<c>handleNotification</c>)方法的<c>Observer</c>对象， 
	///         并为<c>listNotificationInterests</c>返回的每个<c>INotification</c>名称注册它作为观察者。 
	///     </para> 
	/// </remarks> 
	/// <seealso cref="INotification"/> 
	public interface IMediator : INotifier
	{
		/// <summary>
		/// 获取或设置 <c>IMediator</c> 实例名称
		/// </summary>
		string MediatorName { get; }

		/// <summary>
		/// 获取或设置 <c>IMediator</c> 的视图组件。
		/// </summary>
		object ViewComponent { get; set; }

		/// <summary> 
		/// 列出<c>INotification</c>的兴趣列表。 
		/// </summary> 
		/// <returns>一个<c>Array</c>，包含此<c>IMediator</c>感兴趣的<c>INotification</c>名称。</returns> 
		string[] ListNotificationInterests();

		/// <summary> 
		/// 处理<c>INotification</c>。 
		/// </summary> 
		/// <param name="notification">要处理的<c>INotification</c></param> 
		void HandleNotification(INotification notification);

		/// <summary> 
		/// 当Mediator被注册时，由View调用 
		/// </summary> 
		void OnRegister();

		/// <summary> 
		/// 当Mediator被移除时，由View调用 
		/// </summary> 
		void OnRemove();
	}
}
