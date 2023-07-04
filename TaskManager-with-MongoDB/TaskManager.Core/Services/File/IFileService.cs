using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Services.File
{
    public interface IFileService
    {
        void ExportTasksAsync(string userId, string path, string format);
    }
}
