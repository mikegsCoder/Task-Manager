using System.ComponentModel.DataAnnotations;
using TaskManager.Infrastructure.Constants;

namespace TaskManager.Infrastructure.Data.Models.DataBaseModels
{
    public class Remark : BaseDeletableModel<string>
    {
        public Remark()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(DatabaseConstants.Remark_Content_MaxLength)]
        public string Content { get; set; }

        public UserTask Task { get; set; }

        [Required]
        public string UserTaskId { get; set; }
    }
}
