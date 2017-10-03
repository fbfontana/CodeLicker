using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CodeLicker.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("it-IT");
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("it-IT");
            NavigationService.Navigate(new Uri("Views/Menu.xaml", UriKind.Relative));

            Frame pageFrame = null;
            var currParent = VisualTreeHelper.GetParent(this);

            while (currParent != null && pageFrame == null)
            {
                pageFrame = currParent as Frame;
                currParent = VisualTreeHelper.GetParent(currParent);
            }

            if (pageFrame != null)
            {
                ((MainWindow)((Grid)pageFrame.Parent).Parent).FrmMain.Source = new Uri("Views/Documents.xaml", UriKind.Relative);
            }
        }

        private void MnuQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MnuSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
