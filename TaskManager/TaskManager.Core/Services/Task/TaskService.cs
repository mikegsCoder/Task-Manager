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
            List<TaskViewModel> tasks;

            if (categorySelector == null && statusSelector == null)
            {
                tasks = await db.Tasks
                    .Where(x => x.UserId == userId && !x.IsDeleted)
                    .OrderBy(x => x.CreatedOn)
                    .Select(x => new TaskViewModel
                    {
                        Id = x.Id,
                        Status = x.Status.Name != "InProgress" ? x.Status.Name : "In Progress",
                        Category = x.Category.Name,
                        Description = x.Description,
                        CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                        IsFinished = x.IsFinished,
                        FinishedOn = x.FinishedOn == null ? "" : x.FinishedOn.GetValueOrDefault().ToString("dd/MM/yyyy")
                    })
                    .ToListAsync();
            }
            else if (categorySelector != null)
            {
                tasks = await db.Tasks
                    .Where(x => x.UserId == userId && !x.IsDeleted && x.Category.Name == categorySelector)
                    .OrderBy(x => x.CreatedOn)
                    .Select(x => new TaskViewModel
                    {
                        Id = x.Id,
                        Status = x.Status.Name != "InProgress" ? x.Status.Name : "In Progress",
                        Category = x.Category.Name,
                        Description = x.Description,
                        CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                        IsFinished = x.IsFinished,
                        FinishedOn = x.FinishedOn == null ? "" : x.FinishedOn.GetValueOrDefault().ToString("dd/MM/yyyy")
                    })
                    .ToListAsync();
            }
            else
            {
                tasks = await db.Tasks
                    .Where(x => x.UserId == userId && !x.IsDeleted && x.Status.Name == statusSelector)
                    .OrderBy(x => x.CreatedOn)
                    .Select(x => new TaskViewModel
                    {
                        Id = x.Id,
                        Status = x.Status.Name != "InProgress" ? x.Status.Name : "In Progress",
                        Category = x.Category.Name,
                        Description = x.Description,
                        CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                        IsFinished = x.IsFinished,
                        FinishedOn = x.FinishedOn == null ? "" : x.FinishedOn.GetValueOrDefault().ToString("dd/MM/yyyy")
                    })
                    .ToListAsync();
            }

            return tasks;
        }
    }
}
