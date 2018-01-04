using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeLicker.ViewModels;

namespace CodeLicker.Helpers
{
    public class SingleActivityDelegateCommand : ICommand
    {
        private readonly Action<ActivityVM> _Action;

        public SingleActivityDelegateCommand(Action<ActivityVM> action)
        {
            _Action = action;
        }

        public void Execute(object parameter)
        {
            _Action(parameter as ActivityVM);
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
