using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace CodeLicker
{
    public partial class MainWindow : Window
    {
        private const string NormalPath = "M8,16 L16,16 L16,8 L8,8 Z";
        private const string MaximizedPath = "M10 10 L14 10 L14 14 L16 14 L16 8 L10 8 Z  M8 16 L14 16 L14 10 L8 10 Z";
        private const int MaximiziationCompensatingBorder = 7;
        private const int FixedBorder = 1;

        public MainWindow()
        {
            InitializeComponent();
            FrmMain.Source = new Uri("Views/WorkingArea.xaml", UriKind.Relative);
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

    }
}
