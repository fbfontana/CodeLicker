using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeLicker.Helpers;
using CodeLicker.Views;
using CLAnalyzer.Models;

namespace CodeLicker.ViewModels
{
    public class WorkingAreaVM : ObservableObject
    {
        #region Fields
        ObservableCollection<ActivityVM> _Items = new ObservableCollection<ActivityVM>();
        public int _CurrentItem;
        #endregion
        #region Properties
        public int CurrentItem
        {
            get
            {
                return _CurrentItem;
            }
            set
            {
                _CurrentItem = value;
                OnPropertyChanged(nameof(CurrentItem));
            }
        }
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
        public ICommand OpenClassCommand
        {
            get { return new SingleClassDelegateCommand(AddClassActivityVM); }
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

        public void AddClassActivityVM(object newClass)
        {
            var CurrentClass = newClass as ClassModel;
            var ClassInstances = Items
                .Select(e => e as ClassActivityVM).ToList();

            if (ClassInstances.Count() > 0)
            {
                var ExistingCurrentClassInstance = (from f in ClassInstances
                          where f != null && f.CurrentClass != null && f.CurrentClass == CurrentClass
                          select f);

                if (ExistingCurrentClassInstance.Count() == 1)
                {
                    CurrentItem = Items.IndexOf(ExistingCurrentClassInstance.Single());
                    return;
                }
            }

            Items.Add(new ClassActivityVM(CurrentClass));
            CurrentItem = Items.Count() - 1;
        }
    }
}
