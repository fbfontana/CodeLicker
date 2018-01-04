using CodeLicker.Infrastructure;
using CodeLicker.Properties;
using CodeLicker.ViewModels;
using CodeLicker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace CodeLicker
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private const string NormalPath = "M8,16 L16,16 L16,8 L8,8 Z";
        private const string MaximizedPath = "M10 10 L14 10 L14 14 L16 14 L16 8 L10 8 Z  M8 16 L14 16 L14 10 L8 10 Z";
        private const int MaximiziationCompensatingBorder = 7;
        private const int FixedBorder = 1;

        public Home()
        {
            InitializeComponent();
            RefreshRecentFilesMenu();
            UpdateWorkingArea();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                return;
            }
            WindowState = WindowState.Normal;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            SetWindowBorderSize();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            SetWindowBorderSize();
        }

        private void SetWindowBorderSize()
        {
            if (WindowState != WindowState.Maximized)
            {
                HideWindowBorder();
                SetBtnMaximizeImage(NormalPath);
                return;
            }
            RestoreWindowBorder();
            SetBtnMaximizeImage(MaximizedPath);
        }

        private void SetBtnMaximizeImage(string path)
        {
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            BtnMaximizePath.Data = (Geometry)converter.ConvertFrom(path);
        }

        private void HideWindowBorder()
        {
            FirstRow.Height = new GridLength(0);
            LastRow.Height = new GridLength(FixedBorder);
            FirstColumn.Width = new GridLength(FixedBorder);
            LastColumn.Width = new GridLength(FixedBorder);
        }

        private void RestoreWindowBorder()
        {
            FirstRow.Height = new GridLength(MaximiziationCompensatingBorder);
            LastRow.Height = new GridLength(MaximiziationCompensatingBorder + FixedBorder);
            FirstColumn.Width = new GridLength(MaximiziationCompensatingBorder + FixedBorder);
            LastColumn.Width = new GridLength(MaximiziationCompensatingBorder + FixedBorder);
        }

        private WorkingAreaVM WorkingAreaVM;
        private Home WorkingAreaV;

        private static RoutedUICommand _OpenSettingsCommand = new RoutedUICommand("Settings", "OpenSettingsCommand", typeof(Home));
        public static RoutedUICommand OpenSettingsCommand
        {
            get { return _OpenSettingsCommand; }
        }
        private void OpenSettings_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenSettings_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenSettings();
        }

        //public Menu()
        //{
        //    InitializeComponent();
        //    RefreshRecentFilesMenu();
        //    UpdateWorkingArea();
        //}

        private void RefreshRecentFilesMenu()
        {
            MnuRecentFiles.Items.Clear();
            var RecentFiles = RegistryManager.ReadPaths();

            MnuRecentFiles.IsEnabled = RecentFiles.Count() > 0;

            if (RecentFiles.Count() > 0)
            {
                foreach (var File in RecentFiles)
                {
                    var RecentFileItem = new MenuItem() { Header = File };
                    RecentFileItem.Click += RecentFileItem_Click;
                    MnuRecentFiles.Items.Add(RecentFileItem);
                }
            }
        }

        private void RecentFileItem_Click(object sender, RoutedEventArgs e)
        {
            var Filename = (sender as MenuItem).Header.ToString();
            RegistryManager.SavePath(Filename);
            AddActivity(new FileActivityVM(Filename));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //SwitchLanguage();
            //UpdateWorkingArea();

            string Filename = OpenFile();
            if (Filename.Length == 0)
            {
                return;
            }

            RegistryManager.SavePath(Filename);
            AddActivity(new FileActivityVM(Filename));
        }

        private void AddActivity(ActivityVM activity)
        {
            if (WorkingAreaVM == null)
            {
                UpdateWorkingArea();
            }

            WorkingAreaVM.Items.Add(activity);
            WorkingAreaV.TabActivities.SelectedIndex = WorkingAreaV.TabActivities.Items.Count - 1;
        }

        private string OpenFile()
        {
            var OpenFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                DefaultExt = ".cs",
                Filter = "C Sharp Files (*.cs)|*.cs|All Files (*.*)|*.*"
            };

            if (OpenFileDialog.ShowDialog() == true)
            {
                return OpenFileDialog.FileName;
            }
            return string.Empty;
        }

        //private void SwitchLanguage()
        //{
        //    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("it-IT");
        //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("it-IT");
        //    NavigationService.Navigate(new Uri("Views/Menu.xaml", UriKind.Relative));
        //}

        private void UpdateWorkingArea()
        {
            WorkingAreaVM = DataContext as WorkingAreaVM;
            WorkingAreaV = this;
        }

        private void MnuQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenSettings()
        {
            MnuSettings_Click(this, null);//Implementation of open file

            KeyGesture CloseCmdKeyGesture = new KeyGesture(Key.L, ModifierKeys.Alt);

            KeyBinding CloseKeyBinding = new KeyBinding(
                OpenSettingsCommand, CloseCmdKeyGesture);

            this.InputBindings.Add(CloseKeyBinding);
        }

        private void MnuSettings_Click(object sender, RoutedEventArgs e)
        {
            var CurrentSettings = new Views.Settings();
            CurrentSettings.ShowDialog();
            CurrentSettings.Close();
        }

        private void MenuItem_AboutClick(object sender, RoutedEventArgs e)
        {
            UpdateWorkingArea();
            if (WorkingAreaVM.Items.Where(f => f is WelcomeActivityVM).Count() == 0)
            {
                AddActivity(new WelcomeActivityVM());
            }
        }

        private void MenuItem_LicenseClick(object sender, RoutedEventArgs e)
        {
            UpdateWorkingArea();
            if (WorkingAreaVM.Items.Where(f => f is LicenseActivityVM).Count() == 0)
            {
                AddActivity(new LicenseActivityVM());
            }
        }

        private void MenuItem_File_Click(object sender, RoutedEventArgs e)
        {
            RefreshRecentFilesMenu();
        }

        private void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var CurrentTabItem = e.Source as TabItem;

            if (CurrentTabItem == null)
                return;

            if (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(CurrentTabItem, CurrentTabItem, DragDropEffects.All);
            }
        }

        private void TabItem_Drop(object sender, DragEventArgs e)
        {
            var TabItemSource = e.Data.GetData(typeof(TabItem)) as TabItem;

            if (TabItemSource == null)
            {
                return;
            }

            var Activities = (DataContext as WorkingAreaVM).Items;
            var IndexOfSource = GetIndexOf(TabItemSource);
            var SourceActivity = Activities.ElementAt(IndexOfSource) as ActivityVM;

            if (e.Source is TabItem)
            {
                var TabItemTarget = e.Source as TabItem;
                if (!TabItemTarget.Equals(TabItemSource))
                {
                    int TargetIndex = GetIndexOf(TabItemTarget);
                    Activities.Remove(SourceActivity);

                    (DataContext as WorkingAreaVM).Items.Insert(TargetIndex, SourceActivity);

                    TabActivities.SelectedIndex = TargetIndex;
                    e.Handled = true;
                    return;
                }
                e.Handled = true;
                return;
            }

            var IsMoveTabToLastPositionBorder = (e.Source is Border) && (e.Source as Border).Name == "BorTopMoveTabToLastPosition";
            IsMoveTabToLastPositionBorder = IsMoveTabToLastPositionBorder || ((e.Source is Border) && (e.Source as Border).Name == "BorRightMoveTabToLastPosition");
            IsMoveTabToLastPositionBorder = IsMoveTabToLastPositionBorder || ((e.Source is Border) && (e.Source as Border).Name == "BorBottomMoveTabToLastPosition");
            var IsMoveTabToFirstPositionBorder = (e.Source is Border) && (e.Source as Border).Name == "BorLeftMoveTabToLastPosition";

            if (e.Source is TabControl || IsMoveTabToLastPositionBorder || IsMoveTabToFirstPositionBorder)
            {
                Activities.Remove(SourceActivity);
                if (IsMoveTabToFirstPositionBorder)
                {
                    var NewActivities = new ObservableCollection<ActivityVM>() { SourceActivity };
                    Activities.ToList().ForEach(f => NewActivities.Add(f));
                    Activities.Clear();
                    NewActivities.ToList().ForEach(g => Activities.Add(g));
                    TabActivities.SelectedIndex = 0;
                }
                else
                {
                    Activities.Add(SourceActivity);
                    TabActivities.SelectedIndex = Activities.Count() - 1;
                }
                e.Handled = true;
                return;
            }

            var IsBorder = (e.Source is Border) && (e.Source as Border).Name == "BorSingleActivity";
            if (IsBorder)
            {
                var CurrentParent = VisualTreeHelper.GetParent(e.Source as Border);
                DependencyObject TabItemTarget = null;
                do
                {
                    TabItemTarget = CurrentParent as TabItem;
                    CurrentParent = VisualTreeHelper.GetParent(CurrentParent);
                } while (CurrentParent != null && TabItemTarget == null);

                var IndexOfCurrentTab = TabActivities.Items.IndexOf((TabItemTarget as TabItem).DataContext);

                var IndexOfFollowingTab = IndexOfSource > IndexOfCurrentTab ? IndexOfCurrentTab + 1 : IndexOfCurrentTab;




                if (IndexOfFollowingTab <= TabActivities.Items.Count - 1)
                {
                    if (!TabItemTarget.Equals(TabItemSource))
                    {
                        int TargetIndex = IndexOfFollowingTab;
                        Activities.Remove(SourceActivity);

                        (DataContext as WorkingAreaVM).Items.Insert(TargetIndex, SourceActivity);

                        TabActivities.SelectedIndex = TargetIndex;
                        e.Handled = true;
                        return;
                    }
                    e.Handled = true;
                }




            }

        }

        private void MoveTab(DragEventArgs e, ObservableCollection<ActivityVM> activities, TabItem tabItemSource, TabItem tabItemTarget, ActivityVM sourceActivity)
        {
            if (!tabItemTarget.Equals(tabItemSource))
            {
                int TargetIndex = GetIndexOf(tabItemTarget);
                activities.Remove(sourceActivity);

                (DataContext as WorkingAreaVM).Items.Insert(TargetIndex, sourceActivity);

                TabActivities.SelectedIndex = TargetIndex;
                e.Handled = true;
                return;
            }
            e.Handled = true;
        }

        private int GetIndexOf(TabItem TabItemSource)
        {
            return (DataContext as WorkingAreaVM).Items.IndexOf(TabItemSource.DataContext as ActivityVM);
        }

        private void CloseButton_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
        }

    }
}
