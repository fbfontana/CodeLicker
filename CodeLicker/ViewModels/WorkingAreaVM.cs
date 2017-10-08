using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeLicker.Helpers;
using CodeLicker.Views;

namespace CodeLicker.ViewModels
{
    public class WorkingAreaVM : ObservableObject
    {
        #region Fields
        ObservableCollection<ActivityVM> _Items = new ObservableCollection<ActivityVM>();
        #endregion
        #region Properties
        public ObservableCollection<ActivityVM> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                _Items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public ICommand CloseTabCommand
        {
            get { return new SingleActiviyDelegateCommand(RemoveActivity); }
        }
        #endregion
        #region Constructors
        public WorkingAreaVM()
        {
            Items = new ObservableCollection<ActivityVM>() { new WelcomeActivityVM() };
        }
        #endregion

        public void RemoveActivity(ActivityVM currentActivity)
        {
            Items.Remove(currentActivity);
        }
    }
}
