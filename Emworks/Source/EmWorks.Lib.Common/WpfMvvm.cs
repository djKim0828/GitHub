using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace EmWorks.Lib.Common
{
    /// <summary>
    /// View Class
    /// </summary>
    public class DelegateCommand : ICommand
    {
        #region Fields

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        #endregion Fields

        #region Constructors

        public DelegateCommand(Predicate<object> _canexecute, Action<object> execute)
            : this()
        {
            _canExecute = _canexecute;
            _execute = execute;
        }

        public DelegateCommand()
        {
        }

        #endregion Constructors

        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion Events

        #region Methods

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// 별도 추가 함.
        /// </summary>
        public void RaiseCanExcuteChanged()
        {
            if (this.CanExecuteChanged != null)
                this.CanExecuteChanged(this, EventArgs.Empty);
        }

        #endregion Methods
    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Methods

        protected virtual bool CanExecute(object parameter)
        {
            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                VerifyPropertyName(propertyName);
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
        }

        #endregion Methods
    }
}
