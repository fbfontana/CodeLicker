﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeLicker.Helpers
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _Action;

        public DelegateCommand(Action action)
        {
            _Action = action;
        }

        public void Execute(object parameter)
        {
            _Action();
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
