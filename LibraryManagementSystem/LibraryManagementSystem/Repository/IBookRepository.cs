using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBookAsync();
        Task<Book> GetBookByTitleAsync(string title);
        Task AddBookAsync(BookModel book);
        Task UpdateBookAsync(BookModel book);
        Task DeleteBookAsync(int id);
    }
}