using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Infrastructure.Data.Configuration
{
    public class UserTaskConfiguration : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> builder)
        {
            builder.HasData(SeedUserTask());
        }

        private IEnumerable<UserTask> SeedUserTask()
        {
            var task = new UserTask
            {
                Id = "23bf9c00-056c-4c72-9fa7-396c28da66c7",
                CategoryId = "669fe84c-19ce-41f9-ba64-2c844d2b851b",
                StatusId = "0a7bead2-d075-4100-b803-05498f07347b",
                UserId = "90b21bc9-9062-4142-b3f9-774e6797e080",
                Description = "Create final project for the WPF course.",
                CreatedOn = DateTime.Now
        };

            return new List<UserTask>() { task };
        }
    }
}
