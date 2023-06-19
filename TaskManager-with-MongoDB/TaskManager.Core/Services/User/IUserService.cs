namespace TaskManager.Core.Services.UserService
{
    public interface IUserService
    {
        Task<bool> IsUsernameAvailableAsync(string username);

        void CreateAsync(string username, string password, string firstName, string lastName);
    }
}
