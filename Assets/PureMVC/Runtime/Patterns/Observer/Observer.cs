using System;

using KiwiFramework.PureMVC.Interfaces;

namespace KiwiFramework.PureMVC.Patterns
{
	/// <summary> 
	/// 基础的 <c>IObserver</c> 实现。 
	/// </summary> 
	/// <remarks> 
	///     <para> 
	///         <c>Observer</c> 是一个封装了感兴趣对象以及当特定 <c>INotification</c> 被广播时应调用的方法的对象。 
	///     </para> 
	///     <para> 
	///         在 PureMVC 中，<c>Observer</c> 类承担以下职责： 
	///         <list type="bullet"> 
	///             <item>封装感兴趣对象的通知（回调）方法。</item> 
	///             <item>封装感兴趣对象的通知上下文（this）。</item> 
	///             <item>提供设置通知方法和上下文的方法。</item> 
	///             <item>提供通知感兴趣对象的方法。</item> 
	///         </list> 
	///     </para> 
	/// </remarks> 
	/// <seealso cref="PureMVC.Core.View"/> 
	/// <seealso cref="Notification"/> 
	public class Observer : IObserver
	{
		/// <summary> 
		/// 构造函数。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         感兴趣对象的通知方法应该接受一个类型为 <c>INotification</c> 的参数。 
		///     </para> 
		/// </remarks> 
		/// <param name="notifyMethod">感兴趣对象的通知方法</param> 
		/// <param name="notifyContext">感兴趣对象的通知上下文</param> 
		public Observer(Action<INotification> notifyMethod, object notifyContext)
		{
			NotifyMethod  = notifyMethod;
			NotifyContext = notifyContext;
		}

		/// <summary> 
		/// 通知感兴趣对象。 
		/// </summary> 
		/// <param name="notification">要传递给感兴趣对象的通知 <c>INotification</c>。</param> 
		public virtual void NotifyObserver(INotification notification)
		{
			NotifyMethod(notification);
		}

		/// <summary> 
		/// 将对象与通知上下文进行比较。 
		/// </summary> 
		/// <param name="obj">要比较的对象</param> 
		/// <returns>指示对象和通知上下文是否相同</returns> 
		public virtual bool CompareNotifyContext(object obj)
		{
			return NotifyContext.Equals(obj);
		}

		/// <summary>
		/// 回调方法
		/// </summary> 
		public Action<INotification> NotifyMethod { get; set; }

		/// <summary>
		/// 上下文对象
		/// </summary> 
		public object NotifyContext { get; set; }
	}
}
