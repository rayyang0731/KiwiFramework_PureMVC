using System;

namespace KiwiFramework.PureMVC.Interfaces
{
	/// <summary> 
	/// PureMVC控制器的接口定义。 
	/// </summary> 
	/// <remarks> 
	///     <para> 
	///         在PureMVC中，<c>IController</c>的实现者遵循“命令和控制器”策略，并承担以下责任： 
	///         <list type="bullet"> 
	///             <item>记住哪些<c>ICommand</c>用于处理哪些<c>INotifications</c>。</item> 
	///             <item>为每个它具有<c>ICommand</c>映射的<c>INotification</c>向<c>View</c>注册自己作为<c>IObserver</c>。</item> 
	///             <item>在<c>View</c>通知时创建一个适当的<c>ICommand</c>实例来处理给定的<c>INotification</c>。</item> 
	///             <item>调用<c>ICommand</c>的<c>execute</c>方法，传入<c>INotification</c>。</item> 
	///         </list> 
	///     </para> 
	/// </remarks> 
	/// <seealso cref="INotification"/> 
	/// <seealso cref="ICommand"/> 
	public interface IController
	{
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
		void RegisterCommand(string notificationName, Func<ICommand> factory);

		/// <summary> 
		/// 如果先前已经注册了一个<c>ICommand</c>来处理给定的<c>INotification</c>，则执行它。 
		/// </summary> 
		/// <param name="notification">一个<c>INotification</c>通知</param>
		void ExecuteCommand(INotification notification);

		/// <summary>
		/// 移除先前注册的<c>ICommand</c>到<c>INotification</c>的映射。
		/// </summary>
		/// <param name="notificationName">要移除<c>ICommand</c>映射的<c>INotification</c>的名称</param>
		void RemoveCommand(string notificationName);

		/// <summary>
		/// 检查是否为给定的通知注册了一个命令
		/// </summary>
		/// <param name="notificationName">要检查的 <c>ICommand</c>映射的<c>INotification</c>的名称</param>
		/// <returns>是否当前为给定的<c>notificationName</c>注册了一个命令。</returns>
		bool HasCommand(string notificationName);
	}
}
