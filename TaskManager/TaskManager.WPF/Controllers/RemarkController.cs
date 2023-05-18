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
                //ShowInvalidInput("Remark content should be between 5 and 75 characters long.");
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
