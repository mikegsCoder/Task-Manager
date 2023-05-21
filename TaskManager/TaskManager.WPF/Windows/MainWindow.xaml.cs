﻿using Microsoft.EntityFrameworkCore.Diagnostics;
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
using TaskManager.Core.Constants;
using TaskManager.Core.Services.RemarkService;
using TaskManager.Core.Services.Task;
using TaskManager.Core.Services.UserService;
using TaskManager.Core.ViewModels.Task;
using TaskManager.WPF.Controllers;
using TaskManager.WPF.DataContexts;

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

        public UserController userController;
        public TaskController taskController;
        public RemarkController remarkController;

        public MainWindowContext context;

        public MainWindow(
            IUserService _userService,
            ITaskService _taskService,
            IRemarkService _remarkService,
            MainWindowContext _context)
        {
            userService = _userService;
            taskService = _taskService;
            remarkService = _remarkService;

            context = _context;

            remarkController = new RemarkController(remarkService);
            taskController = new TaskController(taskService, context);
            userController = new UserController(userService, context);

            InitializeComponent();

            DataContext = context;
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
            statusSelector = "InProgress";

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
                HelloText.Text = string.Format(
                    Messages.Main_Window_Tello_Text,
                    context.user.Username);
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

        }

        private void ExportXml_Click(object sender, RoutedEventArgs e)
        {

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

            context.NoTasks = context.tasks.Count == 0 ? true : false;
        }
    }
}
