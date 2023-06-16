using KiwiFramework.PureMVC.Interfaces;

namespace KiwiFramework.PureMVC.Patterns
{
	/// <summary> 
	/// 基本的<c>IProxy</c>实现。 
	/// </summary> 
	/// <remarks> 
	/// <para> 
	/// 在PureMVC中，<c>Proxy</c>类用于管理应用程序的数据模型的部分。 
	/// </para> 
	/// <para> 
	/// <c>Proxy</c>可能只是管理对本地数据对象的引用，此时与其交互可能涉及同步方式设置和获取其数据。 
	/// </para> 
	/// <para> 
	/// <c>Proxy</c>类还用于封装应用程序与远程服务的交互以保存或检索数据，在这种情况下，我们采用异步习惯用法; 在<c>Proxy</c>上设置数据（或调用方法）并在<c>Proxy</c>从服务检索数据时监听发送的<c>Notification</c>。 
	/// </para> 
	/// </remarks> 
	/// <seealso cref="PureMVC.Core.Model"/> 
	public class Proxy : Notifier, IProxy
	{
		/// <summary>
		/// 代理的名称
		/// </summary> 
		public const string NAME = "Proxy";

		/// <summary> 
		/// 构造函数。 
		/// </summary> 
		/// <param name="proxyName"></param> 
		/// <param name="data"></param> 
		public Proxy(string proxyName, object data = null)
		{
			ProxyName = proxyName ?? NAME;
			if (data != null) Data = data;
		}

		/// <summary> 
		/// 当代理注册时由Model调用 
		/// </summary> 
		public virtual void OnRegister()
		{
		}

		/// <summary> 
		/// 当代理被删除时由Model调用 
		/// </summary> 
		public virtual void OnRemove()
		{
		}

		/// <summary>
		/// 代理名称
		/// </summary> 
		public string ProxyName { get; protected set; }

		/// <summary>
		/// 代理数据
		/// </summary> 
		public object Data { get; set; }
	}
}
