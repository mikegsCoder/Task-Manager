namespace TaskManager.Core.Services.UserService
{
    public interface IUserService
    {
        Task<bool> IsUsernameAvailableAsync(string username);
    }
}
