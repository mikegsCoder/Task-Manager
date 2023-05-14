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

namespace TaskManager.WPF.Windows.Task
{
    /// <summary>
    /// Interaction logic for TaskEditWindow.xaml
    /// </summary>
    public partial class TaskEditWindow : Window
    {
        public string category;
        public string status;

        public TaskEditWindow(TaskViewModel task)
        {
            InitializeComponent();

            TaskDescription.Text = task.Description;

            category = task.Category;
            status = task.Status;

            SetCategoryButton();
            SetStatusButton();
        }

        private void SetCategoryButton()
        {
            switch (category)
            {
                case "Personal":
                    PersonalRadioButton.IsChecked = true;
                    break;
                case "Job":
                    JobRadioButton.IsChecked = true;
                    break;
                case "Family":
                    FamilyRadioButton.IsChecked = true;
                    break;
                case "Other":
                    OtherRadioButton.IsChecked = true;
                    break;
            }
        }

        private void SetStatusButton()
        {
            switch (status)
            {
                case "Awaiting":
                    AwaitingRadioButton.IsChecked = true;
                    break;
                case "In Progress":
                    InProgressRadioButton.IsChecked = true;
                    break;
                case "Finished":
                    FinishedRadioButton.IsChecked = true;
                    break;
                case "Canceled":
                    CanceledRadioButton.IsChecked = true;
                    break;
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Category_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;

            category = pressed.Content.ToString();
        }

        private void Status_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;

            status = pressed.Content.ToString();
        }
    }
}
