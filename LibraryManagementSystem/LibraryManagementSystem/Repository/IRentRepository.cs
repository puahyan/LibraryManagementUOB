using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.Repository
{
    public interface IRentRepository
    {
        Task<IEnumerable<StudentBook>> GetAllRentedBookAsync();
        Task<IEnumerable<StudentBook>> GetRentedBookByIdAsync(string id);
        Task<string> AddRentBookAsync(RentBookModel book);
    }
}