using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using TaskManager.Infrastructure.Data;
using MongoDB.Driver;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;
using TaskManager.Core.Constants;
using MongoDB.Bson;
using TaskManager.Core.ViewModels.Task;
using TaskManager.Core.DTO;

namespace TaskManager.Core.Services.File
{
    public class FileService : IFileService
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase db;

        //private IMongoCollection<User> userCollection;
        private IMongoCollection<UserTask> taskCollection;
        private IMongoCollection<Status> statusCollection;
        private IMongoCollection<Category> categoryCollection;
        private IMongoCollection<Remark> remarkCollection;

        public FileService(IMongoClient _client)
        {
            client = _client;

            db = client.GetDatabase(DatabaseConstants.DatabaseName);

            //userCollection = db.GetCollection<User>(DatabaseConstants.UserCollectionName);
            taskCollection = db.GetCollection<UserTask>(DatabaseConstants.TaskCollectionName);
            statusCollection = db.GetCollection<Status>(DatabaseConstants.StatusCollectionName);
            categoryCollection = db.GetCollection<Category>(DatabaseConstants.CategoryCollectionName);
            remarkCollection = db.GetCollection<Remark>(DatabaseConstants.RemarkCollectionName);
        }

        public async void ExportTasksAsync(string userId, string path, string format)
        {
        }

        private async Task<List<TaskDto>> GetTasksData(string userId)
        {
            List<TaskDto> tasks = await taskCollection
                .Find(x => x.UserId.ToString() == userId && !x.IsDeleted)
                .Sort(new BsonDocument("CreatedOn", 1))
                .Project(t => new TaskDto
                {
                    Category = "_" + t.CategoryId.ToString(),
                    Status = "_" + t.StatusId.ToString(),
                    Description = t.Id.ToString() + "_" + t.Description,
                    CreatedOn = t.CreatedOn.ToString(),
                    IsFinished = t.IsFinished,
                    FinishedOn = t.FinishedOn.HasValue ? t.FinishedOn.Value.ToString() : "",
                })
                .ToListAsync();

            tasks.ForEach(t => GetStatusAndCategory(t));
            tasks.ForEach(t => GetRemarks(t));
            tasks.ForEach(t => FixDateTimeFormat(t));

            return tasks;
        }
    }
}
