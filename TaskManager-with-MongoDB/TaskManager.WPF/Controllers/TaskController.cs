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
    }
}
