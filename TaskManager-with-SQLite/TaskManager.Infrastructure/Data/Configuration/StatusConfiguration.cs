using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Infrastructure.Data.Configuration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasData(SeedStatuses());
        }

        private IEnumerable<Status> SeedStatuses()
        {
            var awaiting = new Status
            {
                Id = "f83e8ace-ea2c-48f0-846d-1f86c7eb127d",
                Name = "Awaiting"
            };

            var inProgress = new Status
            {
                Id = "0a7bead2-d075-4100-b803-05498f07347b",
                Name = "In Progress"
            };

            var finished = new Status
            {
                Id = "2b381501-15b1-488b-96e2-950869d68d79",
                Name = "Finished"
            };

            var canceled = new Status
            {
                Id = "d920dc1b-fab5-40cc-a387-81f7059da658",
                Name = "Canceled"
            };

            return new List<Status>() { awaiting, inProgress, finished, canceled };
        }
    }
}
