using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using TaskManager.Core.DTO;
using TaskManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Core.Services.File
{
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext db;

        public FileService(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async void ExportTasksAsync(string userId, string path, string format)
        {
           
        }
    }
}
