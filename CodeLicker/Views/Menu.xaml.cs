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
using CodeLicker.ViewModels;

namespace CodeLicker.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        private WorkingAreaVM WorkingAreaVM;
        private WorkingArea WorkingAreaV;

        public Menu()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SwitchLanguage();

            InitializeWorkingArea();
            string Filename = OpenFile();
            if (Filename.Length == 0)
            {
                return;
            }

            AddActivity(new FileActivityVM(Filename));
        }

        private void AddActivity(ActivityVM activity)
        {
            WorkingAreaVM.Items.Add(activity);
            WorkingAreaV.TabActivity.SelectedIndex = WorkingAreaV.TabActivity.Items.Count - 1;
        }

        private string OpenFile()
        {
            var OpenFileDialog = new Microsoft.Win32.OpenFileDialog();
            OpenFileDialog.DefaultExt = ".cs";
            OpenFileDialog.Filter = "C Sharp Files (*.cs)|*.cs|All Files (*.*)|*.*";

            if (OpenFileDialog.ShowDialog() == true)
            {
                return OpenFileDialog.FileName;
            }
            return string.Empty;
        }

        private void SwitchLanguage()
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("it-IT");
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("it-IT");
            NavigationService.Navigate(new Uri("Views/Menu.xaml", UriKind.Relative));
        }

        private void InitializeWorkingArea()
        {
            Frame pageFrame = null;
            var currParent = VisualTreeHelper.GetParent(this);

            while (currParent != null && pageFrame == null)
            {
                pageFrame = currParent as Frame;
                currParent = VisualTreeHelper.GetParent(currParent);
            }

            if (pageFrame != null)
            {
                WorkingAreaVM = (((((MainWindow)((Grid)pageFrame.Parent).Parent).FrmMain).Content as WorkingArea).DataContext as WorkingAreaVM);
                WorkingAreaV = ((((MainWindow)((Grid)pageFrame.Parent).Parent).FrmMain).Content as WorkingArea);
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
