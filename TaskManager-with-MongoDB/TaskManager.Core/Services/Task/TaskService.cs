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
    }
}
