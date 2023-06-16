namespace KiwiFramework.PureMVC.Interfaces
{
	/// <summary> 
	/// PureMVC 代理的接口定义。 
	/// </summary> 
	/// <remarks> 
	/// <para> 
	/// 在 PureMVC 中，<c>IProxy</c> 实现者承担以下职责: 
	/// <list type="bullet"> 
	/// <item>实现一个公共方法，该方法返回代理的名称。</item> 
	/// <item>提供设置和获取数据对象的方法。</item> 
	/// </list> 
	/// </para> 
	/// <para> 
	/// 另外，<c>IProxy</c> 通常: 
	/// <list type="bullet"> 
	/// <item>维护对一个或多个模型数据的引用。</item> 
	/// <item>提供操作该数据的方法。</item> 
	/// <item>在其模型数据发生更改时生成INotifications。</item> 
	/// <item>将其名称公开为<c>public static const</c>，称为<c>NAME</c>，如果它们不是多次实例化的。</item> 
	/// <item>封装与用于获取和持久化模型数据的本地或远程服务的交互。</item> 
	/// </list> 
	/// </para> 
	/// </remarks> 
	public interface IProxy : INotifier
	{
		/// <summary> 
		/// 获取代理名称 
		/// </summary> 
		string ProxyName { get; }

		/// <summary> 
		/// 获取或设置数据对象 
		/// </summary> 
		object Data { get; set; }

		/// <summary> 
		/// 当代理注册时，由模型调用 
		/// </summary> 
		void OnRegister();

		/// <summary> 
		/// 当代理被移除时，由模型调用 
		/// </summary> 
		void OnRemove();
	}
}
