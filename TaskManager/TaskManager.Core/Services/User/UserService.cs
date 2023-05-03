using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Core.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext _db)
        {
            db = _db;
        }
    }
}
