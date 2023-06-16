using System;

using KiwiFramework.PureMVC.Interfaces;

namespace KiwiFramework.PureMVC.Patterns
{
	/// <summary> 
	/// 基本<c> INotifier </c>实现。 
	/// </summary> 
	/// <remarks> 
	/// <para> 
	/// <c> MacroCommand，Command，Mediator </c>和<c> Proxy </c> 
	/// 都需要发送<c>通知</c>。 
	/// </para> 
	/// <para> 
	/// <c> INotifier </c>接口提供了一个常用方法，称为 
	/// <c> sendNotification </c>，该方法解除了实现代码的负担 
	/// 实际上构造<c> Notifications </c>。 
	/// </para> 
	/// <para> 
	/// 所有上述类都扩展的<c> Notifier </c>类 
	/// 提供了对<c> Facade </c>的初始化引用 
	/// Multiton，这对于方便方法是必需的 
	/// 发送<c>通知</c>，但也简化了实现，因为这些 
	/// 类具有频繁的<c> Facade </c>交互并且通常需要 
	/// 访问外观。 
	/// </para> 
	/// <para> 
	/// 注意：在框架的MultiCore版本中，有一个警告 
	/// 通知器，除非它们具有有效的multitonKey，否则它们无法发送通知或到达facade。 
	/// multitonKey设置为： 
	/// <list type =“bullet”> 
	/// <item>在控制器执行命令时</item> 
	/// <item>在Mediator注册到View时</item> 
	/// <item>在代理向Model注册时。</item> 
	/// </list> 
	/// </para> 
	/// </remarks> 
	/// <seealso cref =“KiwiFramework.PureMVC.Patterns.Proxy”/> 
	/// <seealso cref =“KiwiFramework.PureMVC.Patterns.Facade”/> 
	/// <seealso cref =“KiwiFramework.PureMVC.Patterns.Mediator”/> 
	/// <seealso cref =“KiwiFramework.PureMVC.Patterns.MacroCommand”/> 
	/// <seealso cref =“KiwiFramework.PureMVC.Patterns.SimpleCommand”/> 
	public class Notifier : INotifier
	{
		/// <summary> 
		/// 创建并发送<c> INotification </c>。 
		/// </summary> 
		/// <remarks> 
		/// <para> 
		/// 让我们不必在实现代码中构造新的INotification 
		/// 实例。 
		/// </para> 
		/// </remarks> 
		/// <param name =“notificationName”>要发送的通知的名称</ param> 
		/// <param name =“body”>通知的正文（可选）</ param> 
		/// <param name =“type”>通知的类型（可选）</ param> 
		public virtual void SendNotification(string notificationName, object body = null, string type = null)
		{
			Facade.SendNotification(notificationName, body, type);
		}

		/// <summary> 
		/// 初始化此INotifier实例。 
		/// </summary> 
		/// <remarks> 
		/// <para> 
		/// 这是Notifier获取其multitonKey的方法。 
		/// 调用sendNotification或访问 
		/// 外观将在此方法调用之后失败。 
		/// </para> 
		/// <para> 
		/// Mediators，Commands或Proxies可以覆盖 
		/// 此方法以便尽快发送通知 
		/// 或访问Multiton Facade实例为 
		/// 尽快。他们不能访问facade 
		/// 在其构造函数中，因为此方法不会 
		/// 尚未被调用。 
		/// </para> 
		/// </remarks> 
		/// <param name =“key”>此INotifier要使用的multitonKey</ param> 
		public void InitializeNotifier(string key)
		{
			MultitonKey = key;
		}

		/// <summary>
		/// 返回Multiton Facade实例
		/// </summary> 
		protected IFacade Facade
		{
			get
			{
				if (MultitonKey == null) throw new Exception(MULTITON_MSG);
				return KiwiFramework.PureMVC.Patterns.Facade.GetInstance(MultitonKey, key => new KiwiFramework.PureMVC.Patterns.Facade(key));
			}
		}

		/// <summary>
		/// 此应用程序的Multiton Key
		/// </summary> 
		public string MultitonKey { get; protected set; }

		/// <summary>
		/// 消息常量
		/// </summary> 
		protected string MULTITON_MSG = "multitonKey for this Notifier not yet initialized!";
	}
}
