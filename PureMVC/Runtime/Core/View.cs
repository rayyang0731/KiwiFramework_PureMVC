using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

using KiwiFramework.PureMVC.Interfaces;
using KiwiFramework.PureMVC.Patterns;

namespace KiwiFramework.PureMVC.Core
{
	/// <summary>
	/// <see cref="IView"/> 的实现(多例模式)
	/// </summary>
	public class View : IView
	{
		/// <summary> 
		/// 构造并初始化一个新的 View
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         这个 <see cref="IView"/> 实现是一个多例模式， 
		///         所以你不应该直接调用构造函数， 
		///         而是调用工厂方法
		///			<c>View.GetInstance(multitonKey, key => new View(key))</c> 
		///     </para> 
		/// </remarks> 
		/// <param name="key">视图的键</param> 
		public View(string key)
		{
			multitonKey = key;
			InstanceMap.TryAdd(key, new Lazy<IView>(() => this));
			mediatorMap = new ConcurrentDictionary<string, IMediator>();
			observerMap = new ConcurrentDictionary<string, IList<IObserver>>();
			InitializeView();
		}

		/// <summary> 
		/// 初始化 View 实例。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         由构造函数自动调用，这是您在不覆盖构造函数的情况下初始化实例的机会。 
		///     </para> 
		/// </remarks> 
		protected virtual void InitializeView()
		{
		}

		/// <summary> 
		/// <c>View</c> 工厂方法 
		/// </summary> 
		/// <param name="key">视图的键</param> 
		/// <param name="factory">用于创建 <see cref="IView"/> 的 <c>FuncDelegate</c></param> 
		/// <returns>此键的实例</returns> 
		public static IView GetInstance(string key, Func<string, IView> factory)
		{
			return InstanceMap.GetOrAdd(key, new Lazy<IView>(() => factory(key))).Value;
		}

		/// <summary> 
		///     注册一个 <c>IObserver</c> 以便在给定名称的 <c>INotifications</c> 通知时收到通知。 
		/// </summary> 
		/// <param name="notificationName">要通知此 <c>IObserver</c> 的 <c>INotifications</c> 的名称</param> 
		/// <param name="observer">要注册的 <c>IObserver</c></param> 
		public virtual void RegisterObserver(string notificationName, IObserver observer)
		{
			if (observerMap.TryGetValue(notificationName, out var observers))
			{
				observers.Add(observer);
			}
			else
			{
				observerMap.TryAdd(notificationName, new List<IObserver>
				                                     {
					                                     observer
				                                     });
			}
		}

		/// <summary> 
		/// 通知特定 <c>INotification</c> 的 <c>IObservers</c>。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         所有先前附加到此 <c>INotification</c> 列表的 <c>IObservers</c> 都会按照注册的顺序收到通知，并传递一个对 <c>INotification</c> 的引用。 
		///     </para> 
		/// </remarks> 
		/// <param name="notification"></param> 
		public virtual void NotifyObservers(INotification notification)
		{
			// 获取此通知名称的观察者列表的引用
			if (observerMap.TryGetValue(notification.Name, out var observersRef))
			{
				// 将观察者从引用数组复制到工作数组， 
				// 因为在通知循环过程中引用数组可能会发生变化 
				var observers = new List<IObserver>(observersRef);
				foreach (var observer in observers)
				{
					observer.NotifyObserver(notification);
				}
			}
		}

		/// <summary> 
		/// 从给定通知名称的观察者列表中删除给定 notifyContext 的观察者。 
		/// </summary> 
		/// <param name="notificationName">要从中删除的观察者列表</param> 
		/// <param name="notifyContext">删除具有此对象作为其 notifyContext 的观察者</param> 
		public virtual void RemoveObserver(string notificationName, object notifyContext)
		{
			if (observerMap.TryGetValue(notificationName, out var observers))
			{
				for (var i = 0; i < observers.Count; i++)
				{
					if (observers[i].CompareNotifyContext(notifyContext))
					{
						observers.RemoveAt(i);
						break;
					}
				}

				// 同时，当通知的观察者列表长度减少到 
				// 零时，从观察者映射中删除通知键 
				if (observers.Count == 0)
					observerMap.TryRemove(notificationName, out _);
			}
		}

		/// <summary> 
		/// 使用 <c>View</c> 注册一个 <c>IMediator</c> 实例。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         注册 <c>IMediator</c> 以便可以通过名称检索， 
		///         并进一步询问 <c>IMediator</c> 其 
		///         <c>INotification</c> 的兴趣。 
		///     </para> 
		///     <para> 
		///         如果 <c>IMediator</c> 返回任何要通知的 <c>INotification</c> 
		///         名称，将创建一个封装了 
		///         <c>IMediator</c> 实例的 <c>handleNotification</c> 方法 
		///         的 <c>Observer</c> 并将其注册为所有 <c>INotifications</c> 的 <c>Observer</c>，这些 <c>INotifications</c> 是 
		///         <c>IMediator</c> 感兴趣的。 
		///     </para> 
		/// </remarks> 
		/// <param name="mediator">与此 <c>IMediator</c> 实例关联的名称</param> 
		public virtual void RegisterMediator(IMediator mediator)
		{
			if (mediatorMap.TryAdd(mediator.MediatorName, mediator))
			{
				mediator.InitializeNotifier(multitonKey);

				var interests = mediator.ListNotificationInterests();

				if (interests.Length > 0)
				{
					IObserver observer = new Observer(mediator.HandleNotification, mediator);
					foreach (var interest in interests)
					{
						RegisterObserver(interest, observer);
					}
				}

				// 通知中介已注册 
				mediator.OnRegister();
			}
		}

		/// <summary> 
		/// 从 <c>View</c> 中检索一个 <c>IMediator</c>。 
		/// </summary> 
		/// <param name="mediatorName">要检索的 <c>IMediator</c> 实例的名称。</param> 
		/// <returns>先前使用给定的 <c>mediatorName</c> 注册的 <c>IMediator</c> 实例。</returns> 
		public virtual IMediator GetMediator(string mediatorName)
		{
			return mediatorMap.TryGetValue(mediatorName, out var mediator) ? mediator : null;
		}

		/// <summary> 
		/// 从视图中删除一个IMediator。 
		/// </summary> 
		/// <param name="mediatorName">要删除的IMediator实例的名称。</param> 
		/// <returns>从视图中删除的IMediator。</returns> 
		public virtual IMediator RemoveMediator(string mediatorName)
		{
			if (mediatorMap.TryRemove(mediatorName, out var mediator))
			{
				var interests = mediator.ListNotificationInterests();
				foreach (var interest in interests)
				{
					RemoveObserver(interest, mediator);
				}
				mediator.OnRemove();
			}
			return mediator;
		}

		/// <summary> 
		/// 检查是否已注册中介器。 
		/// </summary> 
		/// <param name="mediatorName"></param> 
		/// <returns>是否已使用给定的中介器名称注册了中介器。</returns> 
		public virtual bool HasMediator(string mediatorName)
		{
			return mediatorMap.ContainsKey(mediatorName);
		}

		/// <summary> 
		/// 删除一个IView实例。 
		/// </summary> 
		/// <param name="key">要删除的IView实例的multitonKey。</param> 
		public static void RemoveView(string key)
		{
			InstanceMap.TryRemove(key, out _);
		}

		/// <summary>
		/// 此核心的多例键。
		/// </summary> 
		protected readonly string multitonKey;

		/// <summary>
		/// 中介器名称到中介器实例的映射。
		/// </summary> 
		protected readonly ConcurrentDictionary<string, IMediator> mediatorMap;

		/// <summary>
		/// 通知名称到观察者列表的映射。
		/// </summary> 
		protected readonly ConcurrentDictionary<string, IList<IObserver>> observerMap;

		/// <summary>
		/// 多例视图实例映射。
		/// </summary> 
		protected static readonly ConcurrentDictionary<string, Lazy<IView>> InstanceMap = new ConcurrentDictionary<string, Lazy<IView>>();
	}
}
