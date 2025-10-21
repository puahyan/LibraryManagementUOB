using LibraryManagementSystem.Dtos;
using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByTitleAsync(string title);
        Task AddBookAsync(BookModel book);
        Task UpdateBookAsync(BookModel book); 
        Task DeleteBookAsync(int id);
    }
}
