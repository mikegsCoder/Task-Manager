﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Core.Constants;
using TaskManager.Core.Services.RemarkService;
using TaskManager.Core.Services.Task;
using TaskManager.Core.ViewModels.Remark;
using TaskManager.Core.ViewModels.Task;
using static TaskManager.WPF.Windows.MessageBoxes.MessageBoxes;

namespace TaskManager.WPF.Controllers
{
    public class RemarkController
    {
        private readonly IRemarkService remarkService;

        public RemarkController(IRemarkService _remarkService)
        {
            remarkService = _remarkService;
        }
    }
}