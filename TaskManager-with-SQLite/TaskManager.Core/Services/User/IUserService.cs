using TaskManager.Core.ViewModels.User;

namespace TaskManager.Core.Services.User
{
    public interface IUserService
    {
        Task<UserViewModel> GetUserAsync(string username, string password);

        Task<bool> IsUsernameAvailableAsync(string username);

        void CreateAsync(string username, string password, string firstName, string lastName);
    }
}
