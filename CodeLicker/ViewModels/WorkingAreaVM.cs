using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #endregion
        #region Constructors
        public WorkingAreaVM()
        {
            Items = new ObservableCollection<ActivityVM>() { new WelcomeActivityVM()};
        }
        #endregion
    }
}
