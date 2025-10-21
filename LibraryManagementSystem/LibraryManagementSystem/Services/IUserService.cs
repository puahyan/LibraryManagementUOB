using LibraryManagementSystem.Entity;

namespace LibraryManagementSystem.Services
{
    public interface IUserService
    {
        Task<User> GetRolesAsync(string userName);
        Task<bool> IsValidUser(string username, string password);
        Task<string> GenerateJwtToken(string username);
    }
}
