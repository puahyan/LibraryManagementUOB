using LibraryManagementSystem.Dtos;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;

namespace LibraryManagementSystem.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>>GetAllBooksAsync()
        {
            var bookDtos = new List<BookDto>();

            try
            {
                var resultBooks = await _bookRepository.GetAllBookAsync();

                bookDtos = resultBooks
                    .Select(b => new BookDto
                    {
                        BookId = b.BookId,
                        Author = b.Author,
                        Genre = b.Genre,
                        Title = b.Title,
                        PublishedYear = b.PublishedYear,
                        RackColumnNo = b.Rack.RackColumnNo,
                        RackRowNo = b.Rack.RackRowNo,
                        RackNo = b.Rack.RackNo
                    })
                    .ToList();

                return bookDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return bookDtos;
        }

        public async Task<BookDto> GetBookByTitleAsync(string title)
        {
            var bookDto = new BookDto();

            try
            {
                var resultBook = await _bookRepository.GetBookByTitleAsync(title);

                bookDto = new BookDto
                {
                    BookId = resultBook.BookId,
                    Author = resultBook.Author,
                    Genre = resultBook.Genre,
                    Title = resultBook.Title,
                    PublishedYear = resultBook.PublishedYear,
                    RackColumnNo = resultBook.Rack.RackColumnNo,
                    RackRowNo = resultBook.Rack.RackRowNo,
                    RackNo = resultBook.Rack.RackNo
                };

                return bookDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return bookDto;
        }

        public async Task AddBookAsync(BookModel book)
        {
            try
            {
                await _bookRepository.AddBookAsync(book);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task UpdateBookAsync(BookModel book) 
        {
            try
            {
                await _bookRepository.UpdateBookAsync(book);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteBookAsync(int id) 
        {
            try
            {
                await _bookRepository.DeleteBookAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }  
    }
}