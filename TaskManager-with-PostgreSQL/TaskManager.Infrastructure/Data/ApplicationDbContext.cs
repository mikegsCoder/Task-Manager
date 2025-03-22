using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System;
using TaskManager.Infrastructure.Data.Configuration;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Remark> Remarks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString:
               "Server=localhost;Port=5432;User Id=postgres;Password=b0823HB;Database=TaskManager;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserTask>()
                .Property(p => p.DeletedOn)
                .HasConversion
                (
                    src => src.Value.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src.Value, DateTimeKind.Utc),
                    dst => dst.Value.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst.Value, DateTimeKind.Utc)
                );

            builder.Entity<UserTask>()
                .Property(p => p.CreatedOn)
                .HasConversion
                (
                    src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                    dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
                );

            builder.Entity<UserTask>()
                .Property(p => p.ModifiedOn)
                .HasConversion
                (
                    src => src.Value.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src.Value, DateTimeKind.Utc),
                    dst => dst.Value.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst.Value, DateTimeKind.Utc)
                );

            builder.Entity<UserTask>()
                .Property(p => p.FinishedOn)
                .HasConversion
                (
                    src => src.Value.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src.Value, DateTimeKind.Utc),
                    dst => dst.Value.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst.Value, DateTimeKind.Utc)
                );

            builder.Entity<Remark>()
                 .Property(p => p.DeletedOn)
                 .HasConversion
                 (
                     src => src.Value.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src.Value, DateTimeKind.Utc),
                     dst => dst.Value.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst.Value, DateTimeKind.Utc)
                 );

            builder.Entity<Remark>()
                .Property(p => p.CreatedOn)
                .HasConversion
                (
                    src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                    dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
                );

            builder.Entity<Remark>()
                .Property(p => p.ModifiedOn)
                .HasConversion
                (
                    src => src.Value.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src.Value, DateTimeKind.Utc),
                    dst => dst.Value.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst.Value, DateTimeKind.Utc)
                );

            builder.Entity<Remark>()
                .Property(p => p.FinishedOn)
                .HasConversion
                (
                    src => src.Value.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src.Value, DateTimeKind.Utc),
                    dst => dst.Value.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst.Value, DateTimeKind.Utc)
                );

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new StatusConfiguration());
            builder.ApplyConfiguration(new RemarkConfiguration());
            builder.ApplyConfiguration(new UserTaskConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
