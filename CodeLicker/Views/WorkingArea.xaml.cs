using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CodeLicker.ViewModels;

namespace CodeLicker.Views
{
    /// <summary>
    /// Interaction logic for WorkingArea.xaml
    /// </summary>
    public partial class WorkingArea : Page
    {
        public WorkingArea()
        {
            InitializeComponent();
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
