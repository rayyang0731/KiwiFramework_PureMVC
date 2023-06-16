namespace KiwiFramework.PureMVC.Interfaces
{
	/// <summary>
	/// PureMVC 命令的接口定义。
	/// </summary>
	/// <seealso cref="INotification"/>
	public interface ICommand : INotifier
	{
		/// <summary> 
		/// 执行<c>ICommand</c>的逻辑以处理给定的<c>INotification</c>。 
		/// </summary> 
		/// <param name="notification">要处理的<c>INotification</c>。</param>  
		void Execute(INotification notification);
	}
}
