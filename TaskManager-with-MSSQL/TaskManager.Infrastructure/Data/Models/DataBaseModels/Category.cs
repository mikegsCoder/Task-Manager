using System.ComponentModel.DataAnnotations;
using TaskManager.Infrastructure.Constants;

namespace TaskManager.Infrastructure.Data.Models.DataBaseModels
{
    public class Category
    {
        public Category()
        {
            Id = Guid.NewGuid().ToString();

            Tasks = new List<UserTask>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(DatabaseConstants.Category_Name_MaxLength)]
        public string Name { get; set; }

        public virtual ICollection<UserTask> Tasks { get; set; }
    }
}
