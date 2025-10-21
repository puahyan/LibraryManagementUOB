using LibraryManagementSystem.Dtos;
using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.Services
{
    public interface IRentService
    {
        Task<IEnumerable<RentBookDto>> GetAllRentedBooksAsync();
        Task<IEnumerable<RentBookDto>> GetStudentBookByIdAsync(string id);
        Task<string> AddRentBookAsync(RentBookModel book);
    }
}
