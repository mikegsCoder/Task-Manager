using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Infrastructure.Data.Configuration
{
    public class RemarkConfiguration : IEntityTypeConfiguration<Remark>
    {
        public void Configure(EntityTypeBuilder<Remark> builder)
        {
            builder.HasData(SeedRemarks());
        }

        private IEnumerable<Remark> SeedRemarks()
        {
            var remark1 = new Remark
            {
                Id = "971138d8-d689-4820-8f23-535d1bac1ade",
                Content = "Create application database with at least 5 database models.",
                UserTaskId = "23bf9c00-056c-4c72-9fa7-396c28da66c7",
                CreatedOn = DateTime.Now
            };

            var remark2 = new Remark
            {
                Id = "6eb3a493-8160-475f-a4f3-62089765c162",
                Content = "Use separate projects for database, business logic and WPF application.",
                UserTaskId = "23bf9c00-056c-4c72-9fa7-396c28da66c7",
                CreatedOn = DateTime.Now
            };

            return new List<Remark>() { remark1, remark2 };
        }
    }
}
