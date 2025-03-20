using System.ComponentModel.DataAnnotations;
using TaskManager.Infrastructure.Constants;

namespace TaskManager.Infrastructure.Data.Models.DataBaseModels
{
    public class UserTask : BaseDeletableModel<string>
    {
        public UserTask()
        {
            Id = Guid.NewGuid().ToString();

            Remarks = new List<Remark>();
        }

        public Category Category { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public Status Status { get; set; }

        [Required]
        public string StatusId { get; set; }

        public User User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(DatabaseConstants.UserTask_Description_MaxLength)]
        public string Description { get; set; }

        public virtual ICollection<Remark> Remarks { get; set; }
    }
}
