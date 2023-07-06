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

        private string SerializeData(List<TaskDto> tasksData, string format)
        {
            string result;

            if (format == "json")
            {
                result = JsonConvert.SerializeObject(tasksData, Newtonsoft.Json.Formatting.Indented);
            }
            else
            {
                var sb = new StringBuilder();

                var serializer = new XmlSerializer(typeof(List<TaskDto>), new XmlRootAttribute("Tasks"));
                serializer.Serialize(new StringWriter(sb), tasksData, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
                result = sb.ToString();
            }

            return result;
        }

        private void GetStatusAndCategory(TaskDto task)
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

        private void GetRemarks(TaskDto task)
        {
            int separatorIndex = task.Description.IndexOf("_");

            string taskId = task.Description.Substring(0, separatorIndex);

            task.Description = task.Description.Substring(separatorIndex + 1);

            task.Remarks = remarkCollection
                        .Find(x => x.TaskId.ToString() == taskId && !x.IsDeleted)
                        .Project(x => new RemarkDto
                        {
                            Content = x.Content,
                            CreatedOn = x.CreatedOn.ToString()
                        })
                        .ToListAsync()
                        .GetAwaiter()
                        .GetResult();
        }

        private void FixDateTimeFormat(TaskDto task)
        {
            task.CreatedOn = EditDateTimeFormat(task.CreatedOn);
            task.FinishedOn = task.FinishedOn != ""
                ? EditDateTimeFormat(task.FinishedOn)
                : "";

            task.Remarks.ForEach(r => r.CreatedOn = EditDateTimeFormat(r.CreatedOn));
        }

        private string EditDateTimeFormat(string dateTime)
        {
            int dotIndex = dateTime.IndexOf(".");
            dateTime = dateTime.Substring(0, dotIndex)
                .Replace("-", ".")
                .Replace("T", " ");

            return dateTime;
        }
    }
}
