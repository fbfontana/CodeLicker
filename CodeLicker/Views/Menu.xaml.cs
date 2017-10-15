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

            UpdateWorkingArea();
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
            WorkingAreaV.TabActivities.SelectedIndex = WorkingAreaV.TabActivities.Items.Count - 1;
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

        private void UpdateWorkingArea()
        {
            Frame PageFrame = null;
            var CurrentParent = VisualTreeHelper.GetParent(this);

            while (CurrentParent != null && PageFrame == null)
            {
                PageFrame = CurrentParent as Frame;
                CurrentParent = VisualTreeHelper.GetParent(CurrentParent);
            }

            if (PageFrame != null)
            {
                WorkingAreaVM = (((((MainWindow)((Grid)PageFrame.Parent).Parent).FrmMain).Content as WorkingArea).DataContext as WorkingAreaVM);
                WorkingAreaV = ((((MainWindow)((Grid)PageFrame.Parent).Parent).FrmMain).Content as WorkingArea);
            }
        }

        private void MnuQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MnuSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_AboutClick(object sender, RoutedEventArgs e)
        {
            UpdateWorkingArea();
            if (WorkingAreaVM.Items.Where(f => f is WelcomeActivityVM).Count() == 0)
            {
                AddActivity(new WelcomeActivityVM());
            }
        }
    }
}
