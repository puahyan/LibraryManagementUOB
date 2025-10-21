using LibraryManagementSystem.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string userName);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetRolesAsync(string userName);
    }
}