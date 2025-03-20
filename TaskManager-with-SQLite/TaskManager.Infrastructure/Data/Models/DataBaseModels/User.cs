using System.ComponentModel.DataAnnotations;
using TaskManager.Infrastructure.Constants;

namespace TaskManager.Infrastructure.Data.Models.DataBaseModels
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();

            Tasks = new List<UserTask>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(DatabaseConstants.User_Username_MaxLength)]
        public string Username { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [MaxLength(DatabaseConstants.User_FirstName_MaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(DatabaseConstants.User_LastName_MaxLength)]
        public string LatName { get; set; } = null!;

        public virtual ICollection<UserTask> Tasks { get; set; }
    }
}
