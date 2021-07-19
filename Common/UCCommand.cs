using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Common
{
    public class UCCommand : ICommand
    {
        /// <summary>
        /// The action (or parameterized action) that will be called when the command is invoked.
        /// </summary>
        protected Action _action = null;
        protected Action<object> _parameterizedAction = null;
        protected CanExecuteObject _canExecuteObj;

        /// <summary>
        /// Initializes a new instance of the <see cref="UCCommand"/> class.
        /// </summary>
        public UCCommand(Action action, CanExecuteObject canExecuteObj = null)
        {
            //  Set the action.
            this._action = action;
            this._canExecuteObj = canExecuteObj;
            if (canExecuteObj != null)
                canExecuteObj.PropertyChanged += OnCanExecutePropertyChanged;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UCCommand"/> class.
        /// </summary>
        public UCCommand(Action<object> parameterizedAction, CanExecuteObject canExecuteObj = null)
        {
            //  Set the action.
            this._parameterizedAction = parameterizedAction;
            this._canExecuteObj = canExecuteObj;
            if (canExecuteObj != null)
                canExecuteObj.PropertyChanged += OnCanExecutePropertyChanged;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="param">The param.</param>
        public virtual void DoExecute(object param)
        {
            //  Invoke the executing command, allowing the command to be cancelled.
            CancelCommandEventArgs args = new CancelCommandEventArgs() { Parameter = param, Cancel = false };
            InvokeExecuting(args);

            //  If the event has been cancelled, bail now.
            if (args.Cancel)
                return;

            //  Call the action or the parameterized action, whichever has been set.
            InvokeAction(param);

            //  Call the executed function.
            InvokeExecuted(new CommandEventArgs() { Parameter = param });
        }

        protected void InvokeAction(object param)
        {
            Action theAction = _action;
            Action<object> theParameterizedAction = _parameterizedAction;
            if (theAction != null)
                theAction();
            else
                theParameterizedAction?.Invoke(param);
        }

        protected void InvokeExecuted(CommandEventArgs args)
        {
            CommandEventHandler executed = Executed;

            //  Call the executed event.
            if (executed != null)
                executed(this, args);
        }

        protected void InvokeExecuting(CancelCommandEventArgs args)
        {
            CancelCommandEventHandler executing = Executing;

            //  Call the executed event.
            if (executing != null)
                executing(this, args);
        }


        /// <summary>
        /// Gets or sets a value indicating whether this instance can execute.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </value>
        public bool CanExecute
        {
            get
            {
                if (_canExecuteObj != null)
                {
                    return _canExecuteObj.CanExecute;
                }
                else
                {
                    return true;
                }
            }
        }

        protected void OnCanExecutePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == _canExecuteObj.CanExecute.ToString())
            {
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, EventArgs.Empty);
                }
            }
        }

        #region ICommand Members

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        void ICommand.Execute(object parameter)
        {
            this.DoExecute(parameter);

        }

        #endregion


        /// <summary>
        /// Occurs when can execute is changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Occurs when the command is about to execute.
        /// </summary>
        public event CancelCommandEventHandler Executing;

        /// <summary>
        /// Occurs when the command executed.
        /// </summary>
        public event CommandEventHandler Executed;
    }

    //用于ICommand接口的CanExecute方法的帮助类
    public class CanExecuteObject : NotifyPropertyChangedBase, ICloneable
    {
        public CanExecuteObject()
        {
            CanExecute = true;
        }

        public bool CanExecute { get; set; }

        public object Clone()
        {
            return new CanExecuteObject() { CanExecute = this.CanExecute };
        }
    }

    /// <summary>
    /// The CommandEventHandler delegate.
    /// </summary>
    public delegate void CommandEventHandler(object sender, CommandEventArgs args);

    /// <summary>
    /// The CancelCommandEvent delegate.
    /// </summary>
    public delegate void CancelCommandEventHandler(object sender, CancelCommandEventArgs args);

    /// <summary>
    /// CommandEventArgs - simply holds the command parameter.
    /// </summary>
    public class CommandEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public object Parameter { get; set; }
    }

    /// <summary>
    /// CancelCommandEventArgs - just like above but allows the event to 
    /// be cancelled.
    /// </summary>
    public class CancelCommandEventArgs : CommandEventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CancelCommandEventArgs"/> command should be cancelled.
        /// </summary>
        /// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
        public bool Cancel { get; set; }
    }
}
