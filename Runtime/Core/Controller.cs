using System;
using System.Collections.Concurrent;

using KiwiFramework.PureMVC.Interfaces;
using KiwiFramework.PureMVC.Patterns;

namespace KiwiFramework.PureMVC.Core
{
	/// <summary>
	/// <see cref="IController"/> 实现(多例模式)
	/// </summary>
	public class Controller : IController
	{
		/// <summary>
		/// 构造并初始化一个新的控制器。
		/// </summary>
		/// <remarks>
		/// 由于此“IController”实现是多例的，因此您不应直接调用构造函数，
		/// 而应调用静态多例工厂方法“Controller.GetInstance(multitonKey, key => new Controller(key))”。
		/// </remarks>
		/// <param name="key">控制器的键</param>
		public Controller(string key)
		{
			multitonKey = key;
			
			InstanceMap.TryAdd(multitonKey, new Lazy<IController>(() => this));
			commandMap = new ConcurrentDictionary<string, Func<ICommand>>();
			
			InitializeController();
		}

		/// <summary>
		/// 初始化 <c>Controller</c> 实例 
		/// </summary>
		/// <remarks>
		///     <para>由构造函数自动调用</para>
		///     <para>
		///         请注意，如果您在应用程序中使用<c>View</c>的子类， 
		///         您还应该子类化<c>Controller</c>并以以下方式覆盖<c>initializeController</c>方法：
		///     </para>
		///     <example>
		///         <code>
		///             // 确保 Controller 正在与我的 IView 实现交互 
		///             public override void InitializeController()
		///             {
		///                 view = MyView.GetInstance(multitonKey, () => new MyView(multitonKey));
		///             }
		///         </code>
		///     </example>
		/// </remarks>
		protected virtual void InitializeController()
		{
			view = View.GetInstance(multitonKey, key => new View(key));
		}

		/// <summary> 
		/// <c>Controller</c> 的工厂方法。 
		/// </summary> 
		/// <param name="key">控制器的键</param> 
		/// <param name="factory"><see cref="IController"/>的<c>FuncDelegate</c></param> 
		/// <returns><c>Controller</c>的实例</returns>
		public static IController GetInstance(string key, Func<string, IController> factory)
		{
			return InstanceMap.GetOrAdd(key, new Lazy<IController>(() => factory(key))).Value;
		}

		/// <summary> 
		/// 如果先前已经注册了一个<c>ICommand</c>来处理给定的<c>INotification</c>，则执行它。 
		/// </summary> 
		/// <param name="notification">一个<c>INotification</c>通知</param>
		public virtual void ExecuteCommand(INotification notification)
		{
			if (commandMap.TryGetValue(notification.Name, out var factory))
			{
				var commandInstance = factory();
				commandInstance.InitializeNotifier(multitonKey);
				commandInstance.Execute(notification);
			}
		}

		/// <summary>
		/// 注册特定的<c>ICommand</c>类作为处理特定<c>INotification</c>的处理程序。
		/// </summary>
		/// <remarks>
		///     <para>
		///         如果已经注册了一个<c>ICommand</c>来处理具有此名称的<c>INotification</c>，则不再使用它，而使用新的<c>Func</c>。
		///     </para>
		///     <para>
		///         仅当第一次为此通知名称注册ICommand时，才会创建新ICommand的Observer。
		///     </para>
		/// </remarks>
		/// <param name="notificationName"><c>INotification</c>的名称</param>
		/// <param name="factory"><c>ICommand</c>的<c>Func Delegate</c></param>
		public virtual void RegisterCommand(string notificationName, Func<ICommand> factory)
		{
			if (commandMap.TryGetValue(notificationName, out _) == false)
			{
				view.RegisterObserver(notificationName, new Observer(ExecuteCommand, this));
			}
			commandMap[notificationName] = factory;
		}

		/// <summary>
		/// 移除先前注册的<c>ICommand</c>到<c>INotification</c>的映射。
		/// </summary>
		/// <param name="notificationName">要移除<c>ICommand</c>映射的<c>INotification</c>的名称</param>
		public virtual void RemoveCommand(string notificationName)
		{
			if (commandMap.TryRemove(notificationName, out _))
			{
				view.RemoveObserver(notificationName, this);
			}
		}

		/// <summary>
		/// 检查是否为给定的通知注册了一个命令
		/// </summary>
		/// <param name="notificationName">要检查的 <c>ICommand</c>映射的<c>INotification</c>的名称</param>
		/// <returns>是否当前为给定的<c>notificationName</c>注册了一个命令。</returns>
		public virtual bool HasCommand(string notificationName)
		{
			return commandMap.ContainsKey(notificationName);
		}

		/// <summary>
		/// 移除一个IController实例
		/// </summary>
		/// <param name="key">要移除的IController实例的multitonKey</param>
		public static void RemoveController(string key)
		{
			InstanceMap.TryRemove(key, out _);
		}

		/// <summary>
		/// 对View的本地引用
		/// </summary>
		protected IView view;

		/// <summary>
		/// 多例键值
		/// </summary>
		protected readonly string multitonKey;

		/// <summary>
		/// 将通知名称映射到命令类引用
		/// </summary>
		protected readonly ConcurrentDictionary<string, Func<ICommand>> commandMap;

		/// <summary>
		/// 多例控制器实例的映射表.
		/// </summary>
		protected static readonly ConcurrentDictionary<string, Lazy<IController>> InstanceMap = new();
	}
}
