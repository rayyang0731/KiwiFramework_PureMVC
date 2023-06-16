using System;
using System.Collections.Generic;

using KiwiFramework.PureMVC.Interfaces;

namespace KiwiFramework.PureMVC.Patterns
{
	/// <summary> 
	/// 一个基本的<c>ICommand</c>实现，用于执行其他<c>ICommand</c>。 
	/// </summary> 
	/// <remarks> 
	///     <para> 
	///         一个<c>MacroCommand</c>维护一个 
	///         <c>ICommand</c>类引用列表，称为<i>SubCommands</i>。 
	///     </para> 
	///     <para> 
	///         当调用<c>execute</c>时，<c>MacroCommand</c> 
	///         实例化并依次调用其<i>SubCommands</i>上的<c>execute</c>。 
	///         每个<i>SubCommand</i>都将传递一个引用，该引用指向最初传递给<c>MacroCommand</c>的 
	///         <c>execute</c>方法的<c>INotification</c>。 
	///     </para> 
	///     <para> 
	///         与<c>SimpleCommand</c>不同，您的子类 
	///         不应该重写<c>execute</c>，而是应该 
	///         重写<c>initializeMacroCommand</c>方法， 
	///         对于要执行的每个<i>SubCommand</i>，调用一次<c>addSubCommand</c>。 
	///     </para> 
	/// </remarks> 
	/// <seealso cref="PureMVC.Core.Controller"/> 
	/// <seealso cref="KiwiFramework.PureMVC.Patterns.Notification"/> 
	/// <seealso cref="SimpleCommand"/> 
	public class MacroCommand : Notifier, ICommand
	{
		/// <summary> 
		/// 构造函数。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         您不应该需要定义一个构造函数， 
		///         而是应该重写<c>initializeMacroCommand</c> 
		///         方法。 
		///     </para> 
		///     <para> 
		///         如果您的子类确实定义了一个构造函数，请确保调用<c>super()</c>。 
		///     </para> 
		/// </remarks> 
		public MacroCommand()
		{
			subcommands = new List<Func<ICommand>>();
			InitializeMacroCommand();
		}

		/// <summary> 
		/// 初始化<c>MacroCommand</c>。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         在您的子类中，重写此方法以 
		///         使用<c>ICommand</c>类引用初始化<c>MacroCommand</c>的<i>SubCommand</i> 
		///         列表，如下所示： 
		///     </para> 
		///     <example> 
		///          
		///             override void InitializeMacroCommand()  
		///             { 
		///                 AddSubCommand(() => new com.me.myapp.controller.FirstCommand()); 
		///                 AddSubCommand(() => new com.me.myapp.controller.SecondCommand()); 
		///                 AddSubCommand(() => new com.me.myapp.controller.ThirdCommand()); 
		///             } 
		///          
		///     </example> 
		///     <para> 
		///         请注意，<i>SubCommand</i>可以是任何<c>ICommand</c>实现者， 
		///         <c>MacroCommand</c>或<c>SimpleCommands</c>都是可以接受的。 
		///     </para> 
		/// </remarks> 
		protected virtual void InitializeMacroCommand()
		{
		}

		/// <summary> 
		/// 添加一个<c>SubCommand</c>。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         <i>SubCommands</i>将以先进先出（FIFO）的顺序被调用。 
		///     </para> 
		/// </remarks> 
		/// <param name="factory">对<c>ICommand</c>的<c>FuncDelegate</c>的引用。</param> 
		protected void AddSubCommand(Func<ICommand> factory)
		{
			subcommands.Add(factory);
		}

		/// <summary> 
		/// 执行此<c>MacroCommand</c>的<i>SubCommands</i>。 
		/// </summary> 
		/// <remarks> 
		///     <para> 
		///         <i>SubCommands</i>将以先进先出（FIFO）的顺序被调用。 
		///     </para> 
		/// </remarks> 
		/// <param name="notification">要传递给每个<i>SubCommand</i>的<c>INotification</c>对象。</param> 
		public virtual void Execute(INotification notification)
		{
			while (subcommands.Count > 0)
			{
				var factory         = subcommands[0];
				var commandInstance = factory();
				commandInstance.InitializeNotifier(MultitonKey);
				commandInstance.Execute(notification);
				subcommands.RemoveAt(0);
			}
		}

		/// <summary>
		/// 子命令列表
		/// </summary> 
		public readonly IList<Func<ICommand>> subcommands;
	}
}
