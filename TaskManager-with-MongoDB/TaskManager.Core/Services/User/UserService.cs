﻿using System.Text;
using System.Security.Cryptography;
using MongoDB.Driver;
using TaskManager.Core.Constants;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;
using TaskManager.Core.ViewModels.User;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Core.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase db;
        private IMongoCollection<User> userCollection;

        public UserService(IServiceProvider services)
        {
            client = services.GetRequiredService<IMongoClient>();

            db = client.GetDatabase(DatabaseConstants.DatabaseName);

            userCollection = db.GetCollection<User>(DatabaseConstants.UserCollectionName);
        }

        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            return await userCollection.Find(x => x.Username == username).FirstOrDefaultAsync() == null;
        }

        public async void CreateAsync(string username, string password, string firstName, string lastName)
        {
            var user = new User
            {
                Username = username,
                PasswordHash = ComputeHash(password),
                FirstName = firstName,
                LastName = lastName
            };

            await userCollection.InsertOneAsync(user);
        }

        public async Task<UserViewModel> GetUserAsync(string username, string password)
        {
            var hashPassword = ComputeHash(password);

            var user = await userCollection
                .Find(x => x.Username == username && x.PasswordHash == hashPassword)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                return new UserViewModel
                {
                    Id = user.Id.ToString(),
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }

            return null;
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
