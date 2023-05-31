namespace KiwiFramework.PureMVC.Patterns
{
	/// <summary>
	/// 泛型 <c>INotification</c> 的实现
	/// </summary>
	/// <typeparam name="T">通知数据的类型</typeparam>
	public class GenericNotification<T> : Notification where T : class, new()
	{
		public T Data { get; set; }

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="name"><c>Notification</c> 实例的名称. (required)</param>
		/// <param name="data"><c>Notification</c> 的数据. (required)</param>
		/// <param name="type"><c>Notification</c> 的类型. (optional)</param>
		public GenericNotification(string name, T data, string type = null) : base(name, null, type)
		{
			Data = data;
			Type = type;
		}
	}
}