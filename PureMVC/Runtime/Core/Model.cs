using System;
using System.Collections.Concurrent;

using KiwiFramework.PureMVC.Interfaces;

namespace KiwiFramework.PureMVC.Core
{
	/// <summary> 
	/// <see cref="IModel"/> 的实现(多例模式)
	/// </summary> 
	public class Model : IModel
	{
		/// <summary> 
		/// 构造并初始化一个新的模型 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         这个<see cref="IModel"/>实现是一个多例，  
		///         所以你不应该直接调用构造函数,
		///         而是调用静态多例工厂方法<c>Model.GetInstance(multitonKey, key => new Model(key))</c> 
		///     </para> 
		/// </remarks> 
		/// <param name="key">模型的键</param> 
		public Model(string key)
		{
			multitonKey = key;
			InstanceMap.TryAdd(key, new Lazy<IModel>(() => this));
			proxyMap = new ConcurrentDictionary<string, IProxy>();
			InitializeModel();
		}

		/// <summary> 
		/// 初始化多例<c>Model</c>实例。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         构造函数自动调用，这是您在子类中初始化多例实例的机会，而无需重写构造函数 
		///     </para> 
		/// </remarks> 
		protected virtual void InitializeModel()
		{
		}

		/// <summary> 
		/// <c>Model</c>多例工厂方法。  
		/// </summary> 
		/// <param name="key">模型的键</param> 
		/// <param name="factory"> <see cref="IModel"/>的<c>FuncDelegate</c> </param> 
		/// <returns>此多例键的实例</returns> 
		public static IModel GetInstance(string key, Func<string, IModel> factory)
		{
			return InstanceMap.GetOrAdd(key, new Lazy<IModel>(() => factory(key))).Value;
		}

		/// <summary> 
		/// 在<c>Model</c>中注册一个<c>IProxy</c>。 
		/// </summary> 
		/// <param name="proxy">代理一个要由<c>Model</c>持有的<c>IProxy</c>。</param>
		public virtual void RegisterProxy(IProxy proxy)
		{
			proxy.InitializeNotifier(multitonKey);
			proxyMap[proxy.ProxyName] = proxy;
			proxy.OnRegister();
		}

		/// <summary> 
		/// 从<c>Model</c>中检索一个<c>IProxy</c>。 
		/// </summary> 
		/// <param name="proxyName"></param> 
		/// <returns>先前使用给定的<c>proxyName</c>注册的<c>IProxy</c>实例。</returns>
		public virtual IProxy GetProxy(string proxyName)
		{
			return proxyMap.TryGetValue(proxyName, out var proxy) ? proxy : null;
		}

		/// <summary> 
		/// 从<c>Model</c>中删除一个<c>IProxy</c>。 
		/// </summary> 
		/// <param name="proxyName">proxyName 要删除的<c>IProxy</c>实例的名称。</param> 
		/// <returns>从<c>Model</c>中删除的<c>IProxy</c></returns> 
		public virtual IProxy RemoveProxy(string proxyName)
		{
			if (proxyMap.TryRemove(proxyName, out var proxy))
			{
				proxy.OnRemove();
			}
			return proxy;
		}

		/// <summary> 
		/// 检查是否注册了代理 
		/// </summary> 
		/// <param name="proxyName"></param> 
		/// <returns>当前是否已使用给定的<c>proxyName</c>注册代理。</returns> 
		public virtual bool HasProxy(string proxyName)
		{
			return proxyMap.ContainsKey(proxyName);
		}

		/// <summary> 
		/// 删除一个IModel实例 
		/// </summary> 
		/// <param name="key">要删除的IModel实例的multitonKey</param> 
		public static void RemoveModel(string key)
		{
			InstanceMap.TryRemove(key, out _);
		}

		/// <summary>
		/// 此核心的多例键
		/// </summary>
		protected readonly string multitonKey;

		/// <summary>
		/// 代理名称到IProxy实例的映射
		/// </summary>
		protected readonly ConcurrentDictionary<string, IProxy> proxyMap;

		/// <summary>
		/// 多例模型实例映射.
		/// </summary>
		protected static readonly ConcurrentDictionary<string, Lazy<IModel>> InstanceMap = new ConcurrentDictionary<string, Lazy<IModel>>();
	}
}
