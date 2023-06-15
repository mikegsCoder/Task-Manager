namespace TaskManager.Core.ViewModels.Task
{
    public class TaskViewModel
    {
        public string Id { get; set; } = null!;

        public string? Category { get; set; }

        public string? Status { get; set; }

        public string? Description { get; set; }

        public string? CreatedOn { get; set; }

        public bool IsFinished { get; set; }

        public string? FinishedOn { get; set; }
    }
}
