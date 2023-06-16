using System;

namespace KiwiFramework.PureMVC.Interfaces
{
	/// <summary> 
	/// PureMVC Observer 的接口定义。 
	/// </summary> 
	/// <remarks> 
	/// <para> 
	/// 在 PureMVC 中，Observer 类承担以下职责： 
	/// <list type="bullet"> 
	/// <item>封装感兴趣对象的通知（回调）方法。</item> 
	/// <item>封装感兴趣对象的通知上下文（this）。</item> 
	/// <item>提供设置通知方法和上下文的方法。</item> 
	/// <item>提供通知感兴趣对象的方法。</item> 
	/// </list> 
	/// </para> 
	/// <para> 
	/// PureMVC 不依赖于底层事件模型，例如 Flash 提供的模型，而 ActionScript 3 没有固有的事件模型。 
	/// </para> 
	/// <para> 
	/// PureMVC 中实现的观察者模式存在的目的是支持应用程序和 MVC 三元组的参与者之间的事件驱动通信。 
	/// </para> 
	/// <para> 
	/// 观察者是一个封装有关感兴趣对象的信息的对象，具有应在广播 INotification 时调用的通知方法。然后，观察者作为通知感兴趣对象的代理。 
	/// </para> 
	/// <para> 
	/// 观察者可以通过调用其 notifyObserver 方法来接收通知，传递实现 INotification 接口的对象，例如 Notification 的子类。 
	/// </para> 
	/// </remarks> 
	/// <seealso cref="IView"/> 
	/// <seealso cref="INotification"/> 
	public interface IObserver
	{
		/// <summary> 
		/// 设置感兴趣对象的通知方法（回调）方法 
		/// </summary> 
		/// <remarks> 
		/// <para> 
		/// 通知方法应该接受一个类型为 INotification 的参数 
		/// </para> 
		/// </remarks> 
		Action<INotification> NotifyMethod { set; }

		/// <summary> 
		/// 设置感兴趣对象的通知上下文（this） 
		/// </summary> 
		object NotifyContext { set; }

		/// <summary> 
		/// 通知感兴趣的对象。 
		/// </summary> 
		/// <param name="notification">要传递给感兴趣对象的通知 INotification</param> 
		void NotifyObserver(INotification notification);

		/// <summary> 
		/// 将给定对象与通知上下文对象进行比较。 
		/// </summary> 
		/// <param name="obj">要比较的对象。</param> 
		/// <returns>指示通知上下文和对象是否相同。</returns> 
		bool CompareNotifyContext(object obj);
	}
}
