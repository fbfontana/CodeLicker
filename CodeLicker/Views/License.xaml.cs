using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CodeLicker.Helpers;
using System.Configuration;
using System.IO;
using System.ComponentModel;

namespace CodeLicker.Views
{
    /// <summary>
    /// Interaction logic for License.xaml
    /// </summary>
    public partial class License : UserControl
    {
        public License()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string myFile = System.IO.Path.Combine(applicationDirectory, ConfigurationManager.AppSettings["LicenseUrl"]);

            LicensePage.Navigate(new Uri( myFile));
        }
    }
}
