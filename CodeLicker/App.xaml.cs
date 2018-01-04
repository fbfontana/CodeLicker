using CodeLicker.Data;
using CodeLicker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CodeLicker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DBManager.Context = new CLDBEntities();
            Thread.CurrentThread.CurrentUICulture = 
                new CultureInfo(DBManager.Context.GUILanguages.First(e => e.IsDefault).Culture);
        }
    }
}
