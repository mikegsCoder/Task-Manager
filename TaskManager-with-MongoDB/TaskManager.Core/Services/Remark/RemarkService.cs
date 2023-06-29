﻿using MongoDB.Bson;
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
    }
}