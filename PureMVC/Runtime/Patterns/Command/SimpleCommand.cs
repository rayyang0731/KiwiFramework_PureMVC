using KiwiFramework.PureMVC.Interfaces;

namespace KiwiFramework.PureMVC.Patterns
{
    /// <summary> 
    /// 基本的<c>ICommand</c>实现。 
    /// </summary> 
    /// <remarks> 
    ///     <para> 
    ///         您的子类应该重写<c>execute</c>方法，在其中处理您的业务逻辑<c>INotification</c>。 
    ///     </para> 
    /// </remarks> 
    /// <seealso cref="PureMVC.Core.Controller"/> 
    /// <seealso cref="KiwiFramework.PureMVC.Patterns.Notification"/> 
    /// <seealso cref="MacroCommand"/> 
    public class SimpleCommand : Notifier, ICommand
    {
        /// <summary> 
        /// 执行给定的<c>INotification</c>所启动的用例。 
        /// </summary> 
        /// <remarks> 
        ///     <para> 
        ///         在命令模式中，应用程序用例通常以某些用户操作开始，该操作导致广播<c>INotification</c>，该<c>INotification</c>由<c>ICommand</c>的<c>execute</c>方法中的业务逻辑处理。 
        ///     </para> 
        /// </remarks> 
        /// <param name="notification">要处理的<c>INotification</c>。</param> 
        public virtual void Execute(INotification notification)
        {
        }
    }
}
