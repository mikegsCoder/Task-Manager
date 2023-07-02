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
using TaskManager.Core.ViewModels.Remark;
using TaskManager.Core.ViewModels.Task;
using TaskManager.WPF.Controllers;

namespace TaskManager.WPF
{
    /// <summary>
    /// Interaction logic for RemarksWindow.xaml
    /// </summary>
    public partial class RemarksWindow : Window
    {
        private RemarkViewModel selectedRemark;

        private readonly RemarkController remarkController;

        public TaskViewModel task;
        public List<RemarkViewModel> remarks;

        public RemarksWindow(
            TaskViewModel _task,
            RemarkController _remarkController)
        {
            InitializeComponent();

            task = _task;
            remarkController = _remarkController;

            TaskDescription.Text = "Remarks for task: " + task.Description;

            UpdateRemarksList();
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            await remarkController.CreateRemarkAsync(task);

            UpdateRemarksList();
        }

        private async void EditBtn_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private async void DelBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void SelectedRemark(object sender, RoutedEventArgs e)
        {
            var selectedItem = sender as ListViewItem;

            selectedRemark = selectedItem.Content as RemarkViewModel;
        }

        private async void UpdateRemarksList()
        {
            var updated = await remarkController.GetRemarksAsync(task.Id);

            if (updated == null)
            {
                return;
            }

            remarks = updated;

            var remarksListView = RemarksListView as ListView;

            remarksListView.Items.Clear();

            if (remarksListView == null)
            {
                return;
            }

            foreach (var remark in remarks)
            {
                var item = new ListViewItem { Content = remark };
                item.Selected += SelectedRemark;

                remarksListView.Items.Add(item);
            }

            selectedRemark = null;

            var remarksLabel = NoRemarksLabel as Label;

            if (remarks != null && remarks.Count == 0)
            {
                remarksLabel.Visibility = Visibility.Visible;
            }
            else
            {
                remarksLabel.Visibility = Visibility.Hidden;
            }
        }
    }
}

