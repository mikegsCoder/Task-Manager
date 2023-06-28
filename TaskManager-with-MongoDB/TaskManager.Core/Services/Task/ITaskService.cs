using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.ViewModels.Task;

namespace TaskManager.Core.Services.Task
{
    public interface ITaskService
    {
        Task<bool> CreateTaskAsync(string userId, string description, string category);

        Task<List<TaskViewModel>> GetTasksAsync(string userId, string categorySelector, string statusSelector);

        Task<bool> EditTaskAsync(string taskId, string description, string category, string status);

        Task<bool> DeleteTaskAsync(string taskId);
    }
}
