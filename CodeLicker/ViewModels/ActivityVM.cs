using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLicker.Helpers;

namespace CodeLicker.ViewModels
{
    public abstract class ActivityVM : ObservableObject
    {
        #region Constants
        #endregion
        #region Fields
        private string _Name;
        #endregion
        #region Properties
        public string TabName
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name == value)
                {
                    return;
                }
                _Name = value;
                OnPropertyChanged(nameof(TabName));
            }
        }
        #endregion
        #region Constructors
        public ActivityVM()
        { }
        #endregion

    }
}
