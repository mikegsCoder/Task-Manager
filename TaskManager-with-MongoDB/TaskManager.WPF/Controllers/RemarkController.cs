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
using TaskManager.WPF.Windows.Remark;
using static TaskManager.WPF.Windows.MessageBoxes.MessageBoxes;

namespace TaskManager.WPF.Controllers
{
    public class RemarkController
    {
        private readonly IRemarkService remarkService;

        public List<RemarkViewModel> remarks;
        public TaskViewModel task;

        public RemarkController(IRemarkService _remarkService)
        {
            remarkService = _remarkService;
        }

        public async Task CreateRemarkAsync(TaskViewModel task)
        {
            RemarkAddWindow remarkAddWindow = new RemarkAddWindow(task);

            if (remarkAddWindow.ShowDialog() == false)
            {
                return;
            }

            string content = remarkAddWindow.RemarkContent.Text.Trim();

            if (!ValidateContent(content))
            {
                return;
            }

            try
            {
                await remarkService.CreateRemarkAsync(task.Id, content);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        public async Task<List<RemarkViewModel>> GetRemarksAsync(string taskId)
        {
            try
            {
                return await remarkService.GetRemarksAsync(taskId);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);

                return null;
            }
        }

        private bool ValidateContent(string content)
        {
            if (string.IsNullOrEmpty(content)
                || content.Length < 5
                || content.Length > 75)
            {
                ShowInvalidInput(string.Format(
                    Messages.Remark_Description_Error_Msg,
                    ValidationConstants.Remark_Content_MinLength,
                    ValidationConstants.Remark_Content_MaxLength));

                return false;
            }

            return true;
        }
    }
}
