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
using MongoDB.Driver;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using System.Collections;
using System.Data.Common;
using TaskManager.Core.Constants;

namespace TaskManager.Core.Services.Task
{
    public class TaskService : ITaskService
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase db;

        private IMongoCollection<User> userCollection;
        private IMongoCollection<UserTask> taskCollection;
        private IMongoCollection<Status> statusCollection;
        private IMongoCollection<Category> categoryCollection;
        private IMongoCollection<Remark> remarkCollection;

        public TaskService(IMongoClient _client)
        {
            client = _client;

            db = client.GetDatabase(DatabaseConstants.DatabaseName);

            userCollection = db.GetCollection<User>(DatabaseConstants.UserCollectionName);
            taskCollection = db.GetCollection<UserTask>(DatabaseConstants.TaskCollectionName);
            statusCollection = db.GetCollection<Status>(DatabaseConstants.StatusCollectionName);
            categoryCollection = db.GetCollection<Category>(DatabaseConstants.CategoryCollectionName);
            remarkCollection = db.GetCollection<Remark>(DatabaseConstants.RemarkCollectionName);
        }

        public async Task<List<TaskViewModel>> GetTasksAsync(string userId, string categorySelector, string statusSelector)
        {
            List<TaskViewModel> tasks;

            if (categorySelector == null && statusSelector == null)
            {
                tasks = await taskCollection
                    .Find(x => x.UserId.ToString() == userId && !x.IsDeleted)
                    .Sort(new BsonDocument("CreatedOn", 1))
                    .Project(x => new TaskViewModel
                    {
                        Id = x.Id.ToString(),
                        Status = "_" + x.StatusId.ToString(),
                        Category = "_" + x.CategoryId.ToString(),
                        Description = x.Description,
                        CreatedOnDate = x.CreatedOn,
                        FinishedOnDate = x.FinishedOn
                    })
                    .ToListAsync();
            }
            else if (categorySelector != null)
            {
                var category = await categoryCollection
                    .Find(x => x.Name == categorySelector)
                    .FirstOrDefaultAsync();

                tasks = await taskCollection
                    .Find(x => x.UserId.ToString() == userId && !x.IsDeleted && x.CategoryId == category.Id)
                    .Sort(new BsonDocument("CreatedOn", 1))
                    .Project(x => new TaskViewModel
                    {
                        Id = x.Id.ToString(),
                        Status = "_" + x.StatusId.ToString(),
                        Category = category.Name,
                        Description = x.Description,
                        CreatedOnDate = x.CreatedOn,
                        FinishedOnDate = x.FinishedOn
                    })
                    .ToListAsync();
            }
            else
            {
                var status = await statusCollection
                    .Find(x => x.Name == statusSelector)
                    .FirstOrDefaultAsync();

                tasks = await taskCollection
                   .Find(x => x.UserId.ToString() == userId && !x.IsDeleted && x.StatusId == status.Id)
                   .Sort(new BsonDocument("CreatedOn", 1))
                   .Project(x => new TaskViewModel
                   {
                       Id = x.Id.ToString(),
                       Status = status.Name,
                       Category = "_" + x.CategoryId.ToString(),
                       Description = x.Description,
                       CreatedOnDate = x.CreatedOn,
                       FinishedOnDate = x.FinishedOn
                   })
                   .ToListAsync();
            }

            tasks.ForEach(t => GetStatusAndCategory(t));

            return tasks;
        }

        public async Task<bool> CreateTaskAsync(string userId, string description, string category)
        {
            var user = await userCollection
                .Find(x => x.Id.ToString() == userId)
                .FirstOrDefaultAsync();

            var taskCategory = await categoryCollection
                .Find(x => x.Name == category)
                .FirstOrDefaultAsync();

            var status = await statusCollection
                .Find(x => x.Name == "Awaiting")
                .FirstOrDefaultAsync();

            var task = new UserTask
            {
                UserId = user.Id,
                CategoryId = taskCategory.Id,
                StatusId = status.Id,
                Description = description,
                CreatedOn = GetDateTime()
            };

            await taskCollection.InsertOneAsync(task);

            return true;
        }

        public async Task<bool> EditTaskAsync(string taskId, string description, string category, string status)
        {
            var taskCategory = await categoryCollection
                .Find(x => x.Name == category)
                .FirstOrDefaultAsync();

            var taskStatus = await statusCollection
                .Find(x => x.Name == status)
                .FirstOrDefaultAsync();

            var taskFilter = Builders<UserTask>.Filter
                .Eq(t => t.Id, new ObjectId(taskId));

            if (status == "Finished")
            {
                var taskUpdate = Builders<UserTask>
                    .Update
                    .Set(t => t.Description, description)
                    .Set(t => t.CategoryId, taskCategory.Id)
                    .Set(t => t.StatusId, taskStatus.Id)
                    .Set(t => t.ModifiedOn, GetDateTime())
                    .Set(t => t.FinishedOn, GetDateTime())
                    .Set(t => t.IsFinished, true);

                var taskResult = await taskCollection
                    .UpdateOneAsync(taskFilter, taskUpdate);
            }
            else
            {
                var taskUpdate = Builders<UserTask>
                    .Update
                    .Set(t => t.Description, description)
                    .Set(t => t.CategoryId, taskCategory.Id)
                    .Set(t => t.StatusId, taskStatus.Id)
                    .Set(t => t.ModifiedOn, GetDateTime())
                    .Set(t => t.FinishedOn, null)
                    .Set(t => t.IsFinished, false);

                var taskResult = await taskCollection
                    .UpdateOneAsync(taskFilter, taskUpdate);
            }

            return true;
        }

        public async Task<bool> DeleteTaskAsync(string taskId)
        {
            // update task:
            var taskFilter = Builders<UserTask>.Filter
                .Eq(t => t.Id, new ObjectId(taskId));

            var taskUpdate = Builders<UserTask>
                .Update
                .Set(t => t.DeletedOn, GetDateTime())
                .Set(t => t.IsDeleted, true);

            var taskResult = await taskCollection
                .UpdateOneAsync(taskFilter, taskUpdate);

            // update remarks:
            var remarkFilter1 = Builders<Remark>
                .Filter.Eq(r => r.TaskId, new ObjectId(taskId));

            var remarkFilter2 = Builders<Remark>
                .Filter.Eq(r => r.IsDeleted, false);

            var remarkUpdate = Builders<Remark>
                .Update
                .Set(r => r.DeletedOn, GetDateTime())
                .Set(r => r.IsDeleted, true);

            var remarkResult = await remarkCollection
                .UpdateManyAsync(remarkFilter1 & remarkFilter2, remarkUpdate);

            return true;
        }

        private DateTime GetDateTime()
        {
            return DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        }

        private void GetStatusAndCategory(TaskViewModel task)
        {
            if (task.Status.StartsWith("_"))
            {
                string statusId = task.Status.Substring(1);
                task.Status = statusCollection
                    .Find(x => x.Id.ToString() == statusId)
                    .FirstOrDefaultAsync()
                    .GetAwaiter()
                    .GetResult()
                    .Name;
            }

            if (task.Category.StartsWith("_"))
            {
                string categoryId = task.Category.Substring(1);
                task.Category = categoryCollection
                    .Find(x => x.Id.ToString() == categoryId)
                    .FirstOrDefaultAsync()
                    .GetAwaiter()
                    .GetResult()
                    .Name;
            }
        }
    }
}
