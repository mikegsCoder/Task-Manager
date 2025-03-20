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
using TaskManager.Core.Services.File;
using TaskManager.Core.Services.Remark;
using TaskManager.Core.Services.Task;
using TaskManager.Core.Services.User;
using TaskManager.Core.ViewModels.Remark;
using TaskManager.Core.ViewModels.Task;
using TaskManager.Core.ViewModels.User;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;
using TaskManager.WPF.DataContexts;
using TaskManager.WPF.Windows.Remark;
using TaskManager.WPF.Windows.Task;
using static TaskManager.WPF.MessageBoxes.MessageBoxes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace TaskManager.WPF.Controllers
{
    public class TaskController
    {
        private readonly ITaskService taskService;
        private readonly IFileService fileService;
        private readonly MainWindowContext context;

        public TaskController(
            ITaskService _taskService,
            IFileService _fileService,
            MainWindowContext _context)
        {
            taskService = _taskService;
            fileService = _fileService;
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

            if(!ValidateDescription(description))
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

        public void ExportTasks(string format)
        {
            try
            {
                var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                CommonFileDialogResult result = dialog.ShowDialog();

                string path = dialog.FileName;

                fileService.ExportTasksAsync(context.user.Id, path, format);

                ShowSuccess($"Your tasks successfully exported in {path}\\Tasks.{format}");
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
                ShowInvalidInput("Task description should be between 5 and 75 characters long.");

                return false;
            }

            return true;
        }
    }
}
