﻿using System;
using System.Threading.Tasks;
using TaskManager.Core.Services.Task;
using TaskManager.Core.ViewModels.Task;
using TaskManager.WPF.DataContexts;
using static TaskManager.WPF.Windows.MessageBoxes.MessageBoxes;
using Microsoft.WindowsAPICodePack.Dialogs;
using TaskManager.Core.Constants;
using TaskManager.WPF.Windows.Task;
using TaskManager.Core.Services.File;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.WPF.Controllers
{
    public class TaskController
    {
        private readonly ITaskService taskService;
        private readonly IFileService fileService;
        private readonly MainWindowContext context;

        public TaskController(IServiceProvider services)
        {
            context = services.GetRequiredService<MainWindowContext>();
            fileService = services.GetRequiredService<IFileService>();
            taskService = services.GetRequiredService<ITaskService>();
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

                ShowSuccess(string.Format(Messages.Task_Export_Success_Msg, path, format));
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
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
            string status = taskEditWindow.status.Replace(" ", "");

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
