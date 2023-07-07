using System.Windows;
using TaskManager.Core.ViewModels.Remark;
using TaskManager.Core.ViewModels.Task;

namespace TaskManager.WPF.Windows.Remark
{
    /// <summary>
    /// Interaction logic for RemarkDeleteWindow.xaml
    /// </summary>
    public partial class RemarkDeleteWindow : Window
    {
        private RemarkViewModel remark;
        private TaskViewModel task;

        public RemarkDeleteWindow(
            RemarkViewModel _remark,
            TaskViewModel _task)
        {
            InitializeComponent();

            remark = _remark;
            task = _task;

            TaskDescription.Text = "Delete remark for task: " + task.Description;
            RemarkContent.Text = remark.Content;
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
