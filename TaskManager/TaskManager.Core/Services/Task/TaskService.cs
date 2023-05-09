using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using TaskManager.Core.ViewModels.Task;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Core.Services.Task
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext db;

        public TaskService(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<List<TaskViewModel>> GetTasksAsync(string userId, string categorySelector, string statusSelector)
        {
            return new List<TaskViewModel>();
        }
    }
}
