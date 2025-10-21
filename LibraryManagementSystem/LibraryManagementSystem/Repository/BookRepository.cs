using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBookAsync()
        {
            var resultBooks = await _context.Books
               .Include(b => b.Rack)
               //.Include(b => b.StudentBooks)
               .ToListAsync();

            return resultBooks;
        }

        public async Task<Book>GetBookByTitleAsync(string title)
        {
            var resultBook = await _context.Books
                .Include(b => b.Rack)
                .FirstOrDefaultAsync(b => b.Title.Contains(title));

            return resultBook; 
        }

        public async Task AddBookAsync(BookModel bookModel)
        {
            var bookEntity = new Book
            {
                Title = bookModel.Title,
                Author = bookModel.Author,
                PublishedYear = int.TryParse(bookModel.PublishedYear, out var year) ? year : 0,
                Genre = bookModel.Genre,
                RackId = bookModel.RackId,
                RentPeriod = bookModel.RentPeriod,
            };

            _context.Books.Add(bookEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(BookModel bookModel)
        {
           var resultBook = await _context.Books
           .FirstOrDefaultAsync(b => b.BookId == bookModel.BookId);

            if (resultBook == null)
            {
                return;
            }

            resultBook.Title = bookModel.Title;
            resultBook.Author = bookModel.Author;
            resultBook.PublishedYear = int.TryParse(bookModel.PublishedYear, out var year) ? year : 0;
            resultBook.Genre = bookModel.Genre;
            resultBook.RackId = bookModel.RackId;
            resultBook.RentPeriod = bookModel.RentPeriod;
              
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}