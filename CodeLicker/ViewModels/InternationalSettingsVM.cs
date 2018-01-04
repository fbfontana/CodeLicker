using CodeLicker;
using CodeLicker.Data;
using CodeLicker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLicker.ViewModels
{
    public class InternationalSettingsVM
    {
        private GUILanguage _SelectedLanguage;

        public ObservableCollection<GUILanguage> GUILanguages { get; set; }

        public GUILanguage SelectedLanguage
        {
            get
            {
                return _SelectedLanguage;
            }
            set
            {
                if (_SelectedLanguage != value)
                {
                    _SelectedLanguage = value;
                }
            }
        }

        public InternationalSettingsVM()
        {
            GUILanguages = new ObservableCollection<GUILanguage>();
            DBManager.Context.GUILanguages.ToList().ForEach(e => GUILanguages.Add(e));

            SelectedLanguage = GUILanguages.Where(e => e.IsDefault).Single();

            SettingsManager.CloseSettings += SettingsManager_CloseSettings;
        }

        private void SettingsManager_CloseSettings(object sender, EventArgs e)
        {
            DBManager.Context.GUILanguages.Where(f => f.IsDefault == true).Single().IsDefault = false;
            DBManager.Context.SaveChanges();

            DBManager.Context.GUILanguages.Where(f => f.LanguageId == _SelectedLanguage.LanguageId).Single().IsDefault = true;
            DBManager.Context.SaveChanges();
        }
    }
}
