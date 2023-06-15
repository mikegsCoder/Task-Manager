using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Infrastructure.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedCategories());
        }

        private IEnumerable<Category> SeedCategories()
        {
            var personal = new Category
            {
                Id = "669fe84c-19ce-41f9-ba64-2c844d2b851b",
                Name = "Personal"
            };

            var job = new Category
            {
                Id = "9c24c7ac-b86f-4a30-a0fd-bb3ef6e76308",
                Name = "Job"
            };

            var family = new Category
            {
                Id = "4a9861e0-7884-4d4c-9080-b48de9c883ac",
                Name = "Family"
            };

            var other = new Category
            {
                Id = "43e674b8-ae0e-48a0-a231-e1bc5cf85e9c",
                Name = "Other"
            };

            return new List<Category>() { personal, job, family, other };
        }
    }
}
