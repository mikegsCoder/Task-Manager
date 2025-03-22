using TaskManager.Core.ViewModels.User;

namespace TaskManager.Core.Services.UserService
{
    public interface IUserService
    {
        Task<bool> IsUsernameAvailableAsync(string username);

        void CreateAsync(string username, string password, string firstName, string lastName);

        Task<UserViewModel> GetUserAsync(string username, string password);
    }
}
