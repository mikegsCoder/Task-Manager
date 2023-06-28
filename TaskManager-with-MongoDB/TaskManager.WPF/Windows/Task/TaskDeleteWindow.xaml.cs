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
using TaskManager.Core.ViewModels.Task;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.WPF.Windows.Task
{
    /// <summary>
    /// Interaction logic for TaskDeleteWindow.xaml
    /// </summary>
    public partial class TaskDeleteWindow : Window
    {
        private TaskViewModel task;

        public TaskDeleteWindow(TaskViewModel _task)
        {
            InitializeComponent();

            task = _task;

            HeaderContent.Text = "Are you sure you want to delete the following task?";
            TaskDescription.Text = task.Description;
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
