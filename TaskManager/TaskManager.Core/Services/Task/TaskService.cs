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

        public async Task<bool> CreateTaskAsync(string userId, string description, string category)
        {
            var user = await db.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            var taskCategory = await db.Categories
                .Where(x => x.Name == category)
                .FirstOrDefaultAsync();

            var status = await db.Statuses
                .Where(x => x.Name == "Awaiting")
                .FirstOrDefaultAsync();

            var task = new UserTask
            {
                User = user,
                Category = taskCategory,
                Status = status,
                Description = description,
                CreatedOn = DateTime.Now
            };

            db.Tasks.Add(task);
            await db.SaveChangesAsync();

            return true;
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

        public async Task<bool> EditTaskAsync(string taskId, string description, string category, string status)
        {
            var task = await db.Tasks
               .Where(x => x.Id == taskId)
               .FirstOrDefaultAsync();

            var taskCategory = await db.Categories
               .Where(x => x.Name == category)
               .FirstOrDefaultAsync();

            var taskStatus = await db.Statuses
                .Where(x => x.Name == status)
                .FirstOrDefaultAsync();

            task.Description = description;
            task.Category = taskCategory;
            task.Status = taskStatus;
            task.ModifiedOn = DateTime.Now;

            if (status == "Finished")
            {
                task.FinishedOn = DateTime.Now;
                task.IsFinished = true;
            }
            else
            {
                task.FinishedOn = null;
                task.IsFinished = false;
            }

            db.Update(task);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTaskAsync(string taskId)
        {
            var task = await db.Tasks
               .Where(x => x.Id == taskId)
               .Include(x => x.Remarks)
               .FirstOrDefaultAsync();

            foreach (var remark in task.Remarks)
            {
                remark.DeletedOn = DateTime.Now;
                remark.IsDeleted = true;
            }

            db.Remarks.UpdateRange(task.Remarks);

            task.DeletedOn = DateTime.Now;
            task.IsDeleted = true;

            db.Tasks.Update(task);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
