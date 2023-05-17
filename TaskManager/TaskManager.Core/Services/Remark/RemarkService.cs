﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.ViewModels.Remark;
using TaskManager.Core.ViewModels.Task;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Core.Services.RemarkService
{
    public class RemarkService : IRemarkService
    {
        private readonly ApplicationDbContext db;

        public RemarkService(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<List<RemarkViewModel>> GetRemarksAsync(string taskId)
        {
            var remarks = await db.Remarks
               .Where(x => x.UserTaskId == taskId && !x.IsDeleted)
               .OrderBy(x => x.CreatedOn)
               .Select(x => new RemarkViewModel
               {
                   Id = x.Id,
                   TaskId = x.UserTaskId,
                   Content = x.Content,
                   CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy")
               })
               .ToListAsync();
               
            return remarks;
        }
    }
}