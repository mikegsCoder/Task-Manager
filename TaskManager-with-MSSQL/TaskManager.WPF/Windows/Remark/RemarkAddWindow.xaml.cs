using System.Windows;
using TaskManager.Core.ViewModels.Task;

namespace TaskManager.WPF.Windows.Remark
{
    /// <summary>
    /// Interaction logic for RemarkAddWindow.xaml
    /// </summary>
    public partial class RemarkAddWindow : Window
    {
        private TaskViewModel task;

        public RemarkAddWindow(TaskViewModel _task)
        {
            InitializeComponent();

            task = _task;

            TaskDescription.Text = "Add remark for task: " + task.Description;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
