using LibraryManagementSystem.Dtos;
using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;

namespace LibraryManagementSystem.Services
{
    public class RentService : IRentService
    {
        private readonly IRentRepository _rentRepository;
        private readonly IDynatraceLoggerService _logger;
        public RentService(IRentRepository rentRepository, IDynatraceLoggerService logger)
        {
            _rentRepository = rentRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<RentBookDto>> GetAllRentedBooksAsync()
        {
            var bookDtos = new List<RentBookDto>();

            try
            {
                var resultBooks = await _rentRepository.GetAllRentedBookAsync();

                bookDtos = resultBooks
                    .Select(b => new RentBookDto
                    {
                        BookId = b.BookId,
                        Author = b.Book.Author,
                        Genre = b.Book.Genre,
                        Title = b.Book.Title,
                        RentPeriod = b.Book.RentPeriod,
                        PublishedYear = b.Book.PublishedYear,
                        UserName = b.User.UserName,
                        UserIdentity = b.User.UserCardIdentity,
                        BorrowedDate = b.BorrowedDate,
                        ReturnedDate = b.ReturnedDate,
                    })
                    .ToList();
                await _logger.LogAsync("GetAllRentedBooksAsync ", bookDtos);
                return bookDtos;
            }
            catch (Exception ex)
            {
                await _logger.LogAsync("GetAllRentedBooksAsync ", ex.Message);
            }

            return bookDtos;
        }

        public async Task<IEnumerable<RentBookDto>> GetStudentBookByIdAsync(string id)
        {
            var bookDtos = new List<RentBookDto>();

            try
            {
                var resultBooks = await _rentRepository.GetRentedBookByIdAsync(id);

                bookDtos = resultBooks
                    .Select(b => new RentBookDto
                    {
                        BookId = b.BookId,
                        Author = b.Book.Author,
                        Genre = b.Book.Genre,
                        Title = b.Book.Title,
                        PublishedYear = b.Book.PublishedYear,
                        UserName = b.User.UserName,
                        UserIdentity = b.User.UserCardIdentity,
                        BorrowedDate = b.BorrowedDate,
                        ReturnedDate = b.ReturnedDate,
                    })
                    .ToList();

                return bookDtos;
            }
            catch (Exception ex)
            {
                await _logger.LogAsync("GetStudentBookByIdAsync ", ex.Message);
            }

            return bookDtos;
        }

        public async Task<string> AddRentBookAsync(RentBookModel book)
        {
            string resultMessage = "";
            try
            {
                resultMessage = await _rentRepository.AddRentBookAsync(book);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            resultMessage = "Invalid Operation";
            await _logger.LogAsync("AddRentBookAsync ", resultMessage);
            return resultMessage;
        }
    }
}