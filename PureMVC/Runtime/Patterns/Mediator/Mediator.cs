using KiwiFramework.PureMVC.Interfaces;

namespace KiwiFramework.PureMVC.Patterns
{
	/// <summary>
	/// <c>IMediator</c>接口的基本实现。
	/// </summary>
	/// <seealso cref="PureMVC.Core.View"/>
	public class Mediator : Notifier, IMediator
	{
		/// <summary> 
		/// <c>Mediator</c>的名称。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         通常，<c>Mediator</c>将被编写为服务于一个特定的控件或控件组， 
		///         因此不需要动态命名。 
		///     </para> 
		/// </remarks>
		public const string NAME = "Mediator";

		/// <summary>
		/// 构造函数.
		/// </summary>
		/// <param name="mediatorName"><c>Mediator</c>的名称。 </param>
		/// <param name="viewComponent">服务的组件对象</param>
		public Mediator(string mediatorName, object viewComponent = null)
		{
			MediatorName  = mediatorName ?? NAME;
			ViewComponent = viewComponent;
		}

		/// <summary>
		/// 列出此<c>Mediator</c>感兴趣接收通知的<c>INotification</c>名称。
		/// </summary>
		/// <returns><c>INotification</c>名称列表</returns>
		public virtual string[] ListNotificationInterests()
		{
			return new string[0];
		}

		/// <summary> 
		/// 处理<c>INotification</c>。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         通常情况下，这将在switch语句中处理，每个<c>INotification</c>都有一个'case'条目， 
		///         <c>Mediator</c>感兴趣。 
		///     </para> 
		/// </remarks> 
		/// <param name="notification"></param>
		public virtual void HandleNotification(INotification notification)
		{
		}

		/// <summary> 
		/// 当Mediator被注册时，由View调用 
		/// </summary>
		public virtual void OnRegister()
		{
		}

		/// <summary>
		/// Called by the View when the Mediator is removed
		/// </summary>
		public virtual void OnRemove()
		{
		}

		/// <summary> 
		/// 当Mediator被移除时，由View调用 
		/// </summary>
		public string MediatorName { get; protected set; }

		/// <summary>
		/// 视图组件
		/// </summary>
		public object ViewComponent { get; set; }
	}
}
