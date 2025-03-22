using System.ComponentModel.DataAnnotations;

namespace TaskManager.Infrastructure.Data.Models
{
    public abstract class BaseModel<TKey> : IAuditInfo
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsFinished { get; set; }

        public DateTime? FinishedOn { get; set; }
    }
}
