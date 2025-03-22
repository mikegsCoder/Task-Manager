using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;
using System.Text;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(SeedUsers());
        }

        private IEnumerable<User> SeedUsers()
        {
            var peter = new User
            {
                Id = "90b21bc9-9062-4142-b3f9-774e6797e080",
                Username = "peter123",
                FirstName = "Peter",
                LastName = "Petrov",
                PasswordHash = ComputeHash("123456")
            };

            return new List<User>(){ peter };
        }

        private string ComputeHash(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);

            using (var hash = SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var hashedInputStringBuilder = new StringBuilder(128);

                foreach (var b in hashedInputBytes)
                {
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                }

                return hashedInputStringBuilder.ToString();
            }
        }
    }
}

