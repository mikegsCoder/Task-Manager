using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Constants;
using TaskManager.Core.ViewModels.Remark;
using TaskManager.Core.ViewModels.Task;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Core.Services.RemarkService
{
    public class RemarkService : IRemarkService
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase db;

        private IMongoCollection<UserTask> taskCollection;
        private IMongoCollection<Remark> remarkCollection;

        public RemarkService(IMongoClient _client)
        {
            client = _client;

            db = client.GetDatabase(DatabaseConstants.DatabaseName);

            taskCollection = db.GetCollection<UserTask>(DatabaseConstants.TaskCollectionName);
            remarkCollection = db.GetCollection<Remark>(DatabaseConstants.RemarkCollectionName);
        }

        public async Task<bool> CreateRemarkAsync(string taskId, string content)
        {
            var task = await taskCollection
                .Find(x => x.Id.ToString() == taskId)
                .FirstOrDefaultAsync();

            Remark remark = new Remark
            {
                Content = content,
                TaskId = task.Id,
                CreatedOn = GetDateTime()
            };

            await remarkCollection.InsertOneAsync(remark);

            return true;
        }

        public async Task<bool> EditRemarkAsync(string remarkId, string content)
        {
            var remarkFilter = Builders<Remark>
                .Filter.Eq(r => r.Id, new ObjectId(remarkId));

            var remarkUpdate = Builders<Remark>
                .Update
                .Set(r => r.Content, content)
                .Set(r => r.ModifiedOn, GetDateTime());

            var remarkResult = await remarkCollection
                .UpdateOneAsync(remarkFilter, remarkUpdate);

            return remarkResult.ModifiedCount == 1;
        }

        public async Task<bool> DeleteRemarkAsync(string remarkId)
        {
            var remarkFilter = Builders<Remark>
                .Filter.Eq(r => r.Id, new ObjectId(remarkId));

            var remarkUpdate = Builders<Remark>
                .Update
                .Set(r => r.DeletedOn, GetDateTime())
                .Set(r => r.IsDeleted, true);

            var remarkResult = await remarkCollection
                .UpdateOneAsync(remarkFilter, remarkUpdate);

            return remarkResult.ModifiedCount == 1;
        }

        public async Task<List<RemarkViewModel>> GetRemarksAsync(string taskId)
        {
            var remarks = await remarkCollection
              .Find(x => x.TaskId.ToString() == taskId && !x.IsDeleted)
              .Sort(new BsonDocument("CreatedOn", 1))
              .Project(x => new RemarkViewModel
              {
                  Id = x.Id.ToString(),
                  TaskId = x.TaskId.ToString(),
                  Content = x.Content,
                  CreatedOnDate = x.CreatedOn
              })
              .ToListAsync();

            return remarks;
        }

        private DateTime GetDateTime()
        {
            return DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        }
    }
}
