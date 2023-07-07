using System.Windows;
using TaskManager.Core.ViewModels.Task;

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
