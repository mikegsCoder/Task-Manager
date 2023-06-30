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
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private async void EditBtn_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private async void DelBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void SelectedRemark(object sender, RoutedEventArgs e)
        {
           
        }
    }
}

