namespace TaskManager.Infrastructure.Data.Models
{
    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }

        bool IsFinished { get; set; }

        DateTime? FinishedOn { get; set; }
    }
}
