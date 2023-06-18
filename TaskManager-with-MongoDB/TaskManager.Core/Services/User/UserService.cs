using System.Text;
using System.Security.Cryptography;

using MongoDB.Driver;
using TaskManager.Core.Constants;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Core.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase db;
        private IMongoCollection<User> userCollection;

        public UserService(IMongoClient _client)
        {
            client = _client;

            db = client.GetDatabase(DatabaseConstants.DatabaseName);

            userCollection = db.GetCollection<User>(DatabaseConstants.UserCollectionName);
        }

        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            return await userCollection.Find(x => x.Username == username).FirstOrDefaultAsync() == null;
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
