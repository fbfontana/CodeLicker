using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLicker.Infrastructure
{
    public static class SettingsManager
    {
        public static event EventHandler CloseSettings;

        public static void OnSaveSettings()
        {
            EventHandler SaveSettingsHandler = CloseSettings;
            SaveSettingsHandler(null, EventArgs.Empty);
        }
    }
}
