using Microsoft.EntityFrameworkCore.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManager.Core.Services.File;
using TaskManager.Core.Services.Remark;
using TaskManager.Core.Services.Task;
using TaskManager.Core.Services.User;
using TaskManager.Core.ViewModels.Task;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;
using TaskManager.WPF.Controllers;
using TaskManager.WPF.DataContexts;
using static TaskManager.WPF.MessageBoxes.MessageBoxes;

namespace TaskManager.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string categorySelector;
        private string statusSelector;

        private TaskViewModel selectedTask;

        private readonly IUserService userService;
        private readonly ITaskService taskService;
        private readonly IRemarkService remarkService;
        private readonly IFileService fileService;

        public UserController userController;
        public RemarkController remarkController;
        public TaskController taskController;

        public MainWindowContext context;

        public MainWindow(
            IUserService _userService,
            ITaskService _taskService,
            IRemarkService _remarkService,
            IFileService _fileService,
            MainWindowContext _context)
        {
            userService = _userService;
            taskService = _taskService;
            remarkService = _remarkService;
            fileService = _fileService;

            context = _context;

            remarkController = new RemarkController(remarkService);
            taskController = new TaskController(taskService, fileService, context);
            userController = new UserController(userService, context);

            InitializeComponent();

            DataContext = context;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        private void AllTasksBtn_Click(object sender, RoutedEventArgs e)
        {
            categorySelector = null;
            statusSelector = null;

            UpdateTaskList();
        }

        // By Category
        private void PersonalCategory_Click(object sender, RoutedEventArgs e)
        {
            categorySelector = "Personal";
            statusSelector = null;

            UpdateTaskList();
        }
        private void JobCategory_Click(object sender, RoutedEventArgs e)
        {
            categorySelector = "Job";
            statusSelector = null;

            UpdateTaskList();
        }
        private void FamilyCategory_Click(object sender, RoutedEventArgs e)
        {
            categorySelector = "Family";
            statusSelector = null;

            UpdateTaskList();
        }
        private void OtherCategory_Click(object sender, RoutedEventArgs e)
        {
            categorySelector = "Other";
            statusSelector = null;

            UpdateTaskList();
        }

        // By Status:
        private void AwaitingStatus_Click(object sender, RoutedEventArgs e)
        {
            categorySelector = null;
            statusSelector = "Awaiting";

            UpdateTaskList();
        }
        private void InProgressStatus_Click(object sender, RoutedEventArgs e)
        {
            categorySelector = null;
            statusSelector = "In Progress";

            UpdateTaskList();
        }
        private void FinishedStatus_Click(object sender, RoutedEventArgs e)
        {
            categorySelector = null;
            statusSelector = "Finished";

            UpdateTaskList();
        }
        private void CanceledStatus_Click(object sender, RoutedEventArgs e)
        {
            categorySelector = null;
            statusSelector = "Canceled";

            UpdateTaskList();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            userController.RegisterAsync();
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            await userController.LoginAsync();

            if (context.user != null)
            {
                HelloText.Text = $"Hello {context.user.Username}!";

                ShowGreeting(
                    $"Welcome {context.user.FirstName} {context.user.LastName}.",
                    "Welcome again!");
            }
            else
            {
                HelloText.Text = string.Empty;
            }
        }

        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            await taskController.CreateTaskAsync();

            categorySelector = null;
            statusSelector = null;

            UpdateTaskList();
        }

        private async void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTask == null)
            {
                return;
            }

            await taskController.EditTaskAsync(selectedTask);

            UpdateTaskList();
        }

        private void RemarksBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTask == null)
            {
                return;
            }

            RemarksWindow remarksWindow = new RemarksWindow(selectedTask, remarkController);

            remarksWindow.Show();
        }

        private async void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTask == null)
            {
                return;
            }

            await taskController.DeleteTaskAsync(selectedTask);

            UpdateTaskList();
        }
        
        private void ExportJson_Click(object sender, RoutedEventArgs e)
        {
            taskController.ExportTasks("json");
        }

        private void ExportXml_Click(object sender, RoutedEventArgs e)
        {
            taskController.ExportTasks("xml");
        }

        private void LogoutMouse_Enter(object sender, RoutedEventArgs e)
        {
            var logoutImage = LogoutImage as Image;

            logoutImage.Opacity = 1;
            logoutImage.Width = 28;
            logoutImage.Height = 28;
        }

        private void LogoutMouse_Leave(object sender, RoutedEventArgs e)
        {
            var logoutImage = LogoutImage as Image;

            logoutImage.Opacity = 0.7;
            logoutImage.Width = 26;
            logoutImage.Height = 26;
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowGreeting("See you later!", $"See you {context.user.Username}");

            userController.Logout();

            HelloText.Text = string.Empty;

            TasksListView.Items.Clear();

            selectedTask = null;
        }

        private void SelectedTask(object sender, RoutedEventArgs e)
        {
            var selectedItem = sender as ListViewItem;

            selectedTask = selectedItem.Content as TaskViewModel;
        }

        private async void UpdateTaskList()
        {
            await taskController.GetTasksAsync(categorySelector, statusSelector);

            var taskListView = TasksListView as ListView;

            taskListView.Items.Clear();

            if (taskListView == null)
            {
                return;
            }

            foreach (var task in context.tasks)
            {
                var item = new ListViewItem { Content = task };
                item.Selected += SelectedTask;

                taskListView.Items.Add(item);
            }

            selectedTask = null;

            if (context.tasks.Count == 0)
            {
                context.NoTasks = true;
            }
            else
            {
                context.NoTasks = false;
            }
        }
    }
}
