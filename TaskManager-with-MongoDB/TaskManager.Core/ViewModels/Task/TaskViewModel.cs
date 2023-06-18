using MongoDB.Driver;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Core.ViewModels.Task
{
    public class TaskViewModel
    {
        public string Id { get; set; } = null!;

        public string? Category { get; set; }

        public string? Status { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedOnDate { get; set; }

        public string? CreatedOn 
        { 
            get => CreatedOnDate == null ? "" : CreatedOnDate.Value.ToString("dd/MM/yyyy");
        }

        public bool IsFinished { get => FinishedOnDate != null; }

        public DateTime? FinishedOnDate { get; set; }

        public string? FinishedOn 
        {
            get => FinishedOnDate == null ? "" : FinishedOnDate.Value.ToString("dd/MM/yyyy");
        }
    }
}
