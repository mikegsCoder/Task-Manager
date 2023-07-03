using System;
using System.Collections;
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
