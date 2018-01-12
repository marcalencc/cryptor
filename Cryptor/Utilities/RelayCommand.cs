using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cryptor.Utilities
{
    public class RelayCommand : ICommand
    {
        private Predicate<object> m_canExecute;
        private Action<object> m_execute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            m_canExecute = canExecute;
            m_execute = execute;
        }

        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return m_canExecute == null ? true : m_canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            m_execute(parameter);
        }
    }
}
