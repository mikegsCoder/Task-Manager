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
            var tasksData = GetTasksData(userId);

            var result = SerializeData(tasksData, format);
        }

        private TaskDto[] GetTasksData(string userId)
        {
            TaskDto[] tasks = db.Tasks
                .Where(t => t.UserId == userId && !t.IsDeleted)
                .Include(t => t.Remarks.Where(r => !r.IsDeleted))
                .Include(t => t.Category)
                .Include(t => t.Status)
                .ToArray()
                .Select(t => new TaskDto
                {
                    Category = t.Category.Name,
                    Status = t.Status.Name == "InProgress" ? "In Progress" : t.Status.Name,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    IsFinished = t.IsFinished,
                    FinishedOn = t.FinishedOn.HasValue ? t.FinishedOn.Value.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture) : "",
                    Remarks = t.Remarks
                        .Where(r => !r.IsFinished)
                        .Select(r => new RemarkDto
                        {
                            Content = r.Content,
                            CreatedOn = r.CreatedOn.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                        })
                        .ToArray()
                })
                .OrderBy(t => t.CreatedOn)
                .ToArray();

            return tasks;
        }

        private string SerializeData(TaskDto[] tasksData, string format)
        {
            string result;

            if (format == "json")
            {
                result = JsonConvert.SerializeObject(tasksData, Newtonsoft.Json.Formatting.Indented);
            }
            else
            {
                var sb = new StringBuilder();

                var serializer = new XmlSerializer(typeof(TaskDto[]), new XmlRootAttribute("Tasks"));
                serializer.Serialize(new StringWriter(sb), tasksData, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
                result = sb.ToString();
            }

            return result;
        }
    }
}
