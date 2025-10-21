using LibraryManagementSystem.Entity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryContext _context;

        public UserRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(string userName)
        {
            var result = await _context.Users 
               .FirstOrDefaultAsync(user => user.UserName == userName);

            return result;
        }

        public async Task<User> GetRolesAsync(string userName)
        {
            var result = await _context.Users
               .Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.UserName == userName);

            return result;
        }
    }
}