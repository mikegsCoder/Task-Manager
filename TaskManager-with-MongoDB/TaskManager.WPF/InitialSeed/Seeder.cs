using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Constants;
using TaskManager.Core.Services.Task;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.WPF.InitialSeed
{
    public class Seeder
    {
        public static async Task SeedAsync(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var client = scope.ServiceProvider.GetRequiredService<IMongoClient>();
                
                var db = client.GetDatabase(DatabaseConstants.DatabaseName);

                var userCollection = db.GetCollection<User>(DatabaseConstants.UserCollectionName);
                var taskCollection = db.GetCollection<UserTask>(DatabaseConstants.TaskCollectionName);
                var statusCollection = db.GetCollection<Status>(DatabaseConstants.StatusCollectionName);
                var categoryCollection = db.GetCollection<Category>(DatabaseConstants.CategoryCollectionName);
                var remarkCollection = db.GetCollection<Remark>(DatabaseConstants.RemarkCollectionName);

                var users = await userCollection.Find(new BsonDocument()).ToListAsync();
                if (users.Any()) return;

                // create categories:
                var personal = new Category()
                {
                    Id = new ObjectId("644617f6e795f3972d58970f"),
                    Name = "Personal"
                };

                var job = new Category()
                {
                    Id = new ObjectId("6446180be795f3972d589710"),
                    Name = "Job"
                };

                var family = new Category()
                {
                    Id = new ObjectId("64461827e795f3972d589711"),
                    Name = "Family"
                };

                var other = new Category()
                {
                    Id = new ObjectId("64461841e795f3972d589712"),
                    Name = "Other"
                };

                // create statuses:
                var awaiting = new Status()
                {
                    Id = new ObjectId("64461875e795f3972d589714"),
                    Name = "Awaiting"
                };

                var inProgress = new Status()
                {
                    Id = new ObjectId("6446189ce795f3972d589715"),
                    Name = "In Progress"
                };

                var finished = new Status()
                {
                    Id = new ObjectId("644618b6e795f3972d589716"),
                    Name = "Finished"
                };

                var canceled = new Status()
                {
                    Id = new ObjectId("644618d1e795f3972d589717"),
                    Name = "Canceled"
                };

                // create user:
                var peter = new User()
                {
                    Id = new ObjectId("6446510a152cdd169d10db4e"),
                    FirstName = "Peter",
                    LastName = "Petrov",
                    Username = "peter123",
                    PasswordHash = "BA3253876AED6BC22D4A6FF53D8406C6AD864195ED144AB5C87621B6C233B548BAEAE6956DF346EC8C17F5EA10F35EE3CBC514797ED7DDD3145464E2A0BAB413"
                };

                // create task:
                var task = new UserTask()
                {
                    Id = new ObjectId("643faba39718ec8266b19bf0"),
                    UserId = peter.Id,
                    CategoryId = personal.Id,
                    StatusId = inProgress.Id,
                    Description = "Refactor Task Manager project to use MongoDB.",
                    CreatedOn = GetDateTime()
                };

                // create remarks:
                var firstRemark = new Remark()
                {
                    Id = new ObjectId("6446597cf2616cbeab3a73ee"),
                    TaskId = task.Id,
                    Content = "Use ObjectId as reference between documents.",
                    CreatedOn = GetDateTime()
                };

                var secondRemark = new Remark()
                {
                    Id = new ObjectId("6446654646547db9a8032272"),
                    TaskId = task.Id,
                    Content = "Refactor Database seeding.",
                    CreatedOn = GetDateTime()
                };

                // seed database:
                await categoryCollection.InsertManyAsync(new List<Category> { personal, job, family, other });
                await statusCollection.InsertManyAsync(new List<Status> { awaiting, inProgress, finished, canceled });
                await userCollection.InsertOneAsync(peter);
                await taskCollection.InsertOneAsync(task);
                await remarkCollection.InsertManyAsync(new List<Remark> { firstRemark, secondRemark });
            }
        }

        private static DateTime GetDateTime()
        {
            return DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        }
    }
}
