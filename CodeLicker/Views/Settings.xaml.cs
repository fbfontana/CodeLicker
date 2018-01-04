using CodeLicker.Controls;
using CodeLicker.ViewModels;
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
using System.Windows.Shapes;

namespace CodeLicker.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            DataContext = new SettingsVM();
            InitializeComponent();

            WindowTitle.Content = WindowTitle.Content.ToString().Replace("_", "");
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // va spostato in qualche modo nella VM
        private void TviInternationalSettings_Selected(object sender, RoutedEventArgs e)
        {
            GrdSettings.Children.Add(new InternationalSettings() { DataContext = new InternationalSettingsVM() });
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
