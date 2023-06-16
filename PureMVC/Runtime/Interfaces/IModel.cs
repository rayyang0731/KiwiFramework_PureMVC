namespace KiwiFramework.PureMVC.Interfaces
{
	/// <summary> 
	/// PureMVC Model的接口定义。 
	/// </summary> 
	/// <remarks> 
	///     <para> 
	///         在PureMVC中，<c>IModel</c>实现者通过命名查找提供对<c>IProxy</c>对象的访问。 
	///     </para> 
	///     <para> 
	///         <c>IModel</c>承担以下职责： 
	///         <list type="bullet"> 
	///             <item>维护<c>IProxy</c>实例的缓存</item> 
	///             <item>提供注册、检索和删除<c>IProxy</c>实例的方法</item> 
	///         </list> 
	///     </para> 
	/// </remarks> 
	public interface IModel
	{
		/// <summary> 
		/// 在<c>Model</c>中注册一个<c>IProxy</c>实例。 
		/// </summary> 
		/// <param name="proxy">要由<c>Model</c>持有的对象引用。</param> 
		void RegisterProxy(IProxy proxy);

		/// <summary> 
		/// 从Model中检索一个<c>IProxy</c>实例。 
		/// </summary> 
		/// <param name="proxyName"></param> 
		/// <returns>以前使用给定的<c>proxyName</c>注册的<c>IProxy</c>实例。</returns> 
		IProxy GetProxy(string proxyName);

		/// <summary> 
		/// 从Model中移除一个<c>IProxy</c>实例。 
		/// </summary> 
		/// <param name="proxyName"></param> 
		/// <returns>从<c>Model</c>中移除的<c>IProxy</c>。</returns> 
		IProxy RemoveProxy(string proxyName);

		/// <summary> 
		/// 检查是否已注册代理 
		/// </summary> 
		/// <param name="proxyName"></param> 
		/// <returns>是否当前使用给定的<c>proxyName</c>注册了代理。</returns> 
		bool HasProxy(string proxyName);
	}
}
