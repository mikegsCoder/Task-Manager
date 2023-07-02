﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.ViewModels.Remark;
using TaskManager.Core.ViewModels.Task;

namespace TaskManager.Core.Services.RemarkService
{
    public interface IRemarkService
    {
        Task<List<RemarkViewModel>> GetRemarksAsync(string taskId);

        Task<bool> CreateRemarkAsync(string taskId, string content);

        Task<bool> EditRemarkAsync(string remarkId, string content);
    }
}
