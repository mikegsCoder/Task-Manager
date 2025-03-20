using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

using TaskManager.Infrastructure.Data;
using TaskManager.Core.ViewModels.User;

namespace TaskManager.Core.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async void CreateAsync(string username, string password, string firstName, string lastName)
        {
            var user = new Infrastructure.Data.Models.DataBaseModels.User
            {
                Username = username,
                PasswordHash = ComputeHash(password),
                FirstName = firstName,
                LatName = lastName
            };

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task<UserViewModel> GetUserAsync(string username, string password)
        {
            var hashPassword = ComputeHash(password);

            var user = await db.Users.FirstOrDefaultAsync(x => x.Username == username && x.PasswordHash == hashPassword);

            if (user != null)
            {
                return new UserViewModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LatName
                };
            }

            return null;
        }

        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            return !await db.Users.AnyAsync(x => x.Username == username);
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
