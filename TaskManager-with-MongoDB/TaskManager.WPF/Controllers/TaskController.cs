using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Core.Services.Task;
using TaskManager.Core.Services.UserService;
using TaskManager.Core.ViewModels.Remark;
using TaskManager.Core.ViewModels.Task;
using TaskManager.Core.ViewModels.User;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;
using TaskManager.WPF.DataContexts;
using static TaskManager.WPF.Windows.MessageBoxes.MessageBoxes;
using Microsoft.WindowsAPICodePack.Dialogs;
using TaskManager.Core.Constants;
using TaskManager.WPF.Windows.Task;

namespace TaskManager.WPF.Controllers
{
    public class TaskController
    {
        private readonly ITaskService taskService;
        private readonly MainWindowContext context;

        public TaskController(
            ITaskService _taskService,
            MainWindowContext _context)
        {
            taskService = _taskService;
            context = _context;
        }

        public async Task GetTasksAsync(string categorySelector, string statusSelector)
        {
            try
            {
                context.tasks = await taskService.GetTasksAsync(context.user.Id, categorySelector, statusSelector);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        public async Task CreateTaskAsync()
        {
            TaskAddWindow taskAddWindow = new TaskAddWindow();

            if (taskAddWindow.ShowDialog() == false)
            {
                return;
            }

            string description = taskAddWindow.TaskDescription.Text.Trim();
            string category = taskAddWindow.category;

            if (!ValidateDescription(description))
            {
                return;
            }

            try
            {
                await taskService.CreateTaskAsync(context.user.Id, description, category);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        public async Task EditTaskAsync(TaskViewModel task)
        {
            TaskEditWindow taskEditWindow = new TaskEditWindow(task);

            if (taskEditWindow.ShowDialog() == false)
            {
                return;
            }

            string description = taskEditWindow.TaskDescription.Text.Trim();
            string category = taskEditWindow.category;
            string status = taskEditWindow.status;

            if (!ValidateDescription(description))
            {
                return;
            }

            try
            {
                await taskService.EditTaskAsync(task.Id, description, category, status);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        public async Task DeleteTaskAsync(TaskViewModel task)
        {
            TaskDeleteWindow taskDeleteWindow = new TaskDeleteWindow(task);

            if (taskDeleteWindow.ShowDialog() == false)
            {
                return;
            }

            try
            {
                await taskService.DeleteTaskAsync(task.Id);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private bool ValidateDescription(string description)
        {
            if (string.IsNullOrEmpty(description)
                || description.Length < 5
                || description.Length > 75)
            {
                ShowInvalidInput(string.Format(
                    Messages.Task_Description_Error_Msg,
                    ValidationConstants.UserTask_Description_MinLength,
                    ValidationConstants.UserTask_Description_MaxLength));

                return false;
            }

            return true;
        }
    }
}
