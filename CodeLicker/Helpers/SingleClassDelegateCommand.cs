using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeLicker.ViewModels;
using CLAnalyzer.Models;

namespace CodeLicker.Helpers
{
    public class SingleClassDelegateCommand : ICommand
    {
        private readonly Action<object> _Action;

        public SingleClassDelegateCommand(Action<object> action)
        {
            _Action = action;
        }

        public void Execute(object parameter)
        {
            _Action(parameter as object);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        #pragma warning disable 67
        public event EventHandler CanExecuteChanged;
        #pragma warning restore 67
    }
}
