using KiwiFramework.PureMVC.Interfaces;

namespace KiwiFramework.PureMVC.Patterns
{
	/// <summary>
	/// <c>INotification</c> 的实现
	/// </summary>
	/// <remarks>
	///     <para>
	///			通常，<c>IMediator</c> 实现将事件监听器放置在视图组件上，
	///			然后通过广播 <c>Notification</c> 以触发 <c>ICommand</c> 或与其他 <c>IMediator</c> 的通信.
	///			<c>IProxy</c> 和 <c>ICommand</c> 实例通过广播 <c>INotification</c> 相互通信并与 <c>IMediator</c> 通信。
	///			PureMVC 的 <c>Notification</c> 遵循 “ Publish / Subscribe ” 模式.
	///			PureMVC 中类不需要在父子关系中相互关联，而是使用 <c>Notification</c> 相互通信.
	///     </para>
	/// </remarks>
	/// <seealso cref="Observer"/>
	public class Notification : INotification
	{
		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="name"><c>Notification</c> 实例的名称. (required)</param>
		/// <param name="body"><c>Notification</c> 的数据. (optional)</param>
		/// <param name="type"><c>Notification</c> 的类型. (optional)</param>
		public Notification(string name, object body = null, string type = null)
		{
			Name = name;
			Body = body;
			Type = type;
		}

		/// <summary>
		/// 获取 <c>Notification</c> 实例的字符串表示形式。
		/// </summary>
		/// <returns><c>Notification</c> 实例的字符串表示.</returns>
		public override string ToString()
		{
			var msg = "Notification Name: " + Name;
			msg += "\nBody:" + ((Body == null) ? "null" : Body.ToString());
			msg += "\nType:" + (Type ?? "null");
			return msg;
		}

		/// <summary>
		/// Notification 实例的名称
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Notification 实例的主体
		/// </summary>
		public object Body { get; set; }

		/// <summary>
		/// Notification 实例的类型
		/// </summary>
		public string Type { get; set; }
	}
}
