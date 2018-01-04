using CodeLicker.Helpers;
using CodeLicker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeLicker.ViewModels
{
    public class SettingsVM : ObservableObject
    {
        //public string Namespace
        //{
        //    get
        //    {
        //        return "bbbbbbb";
        //    }
        //    set
        //    {
        //        OnPropertyChanged(nameof(Namespace));
        //    }
        //}
        
        public ICommand SaveCommand
        {
            get { return new DelegateCommand(RaiseSaveSettings); }
        }

        public SettingsVM() { }

        public void RaiseSaveSettings()
        {
            SettingsManager.OnSaveSettings();
        }
    }
}
