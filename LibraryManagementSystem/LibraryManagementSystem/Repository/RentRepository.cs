using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class RentRepository : IRentRepository
    {
        private readonly LibraryContext _context;

        public RentRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentBook>> GetAllRentedBookAsync()
        {
            var resultBooks = await _context.StudentBooks
               .Include(b => b.User)
               .Include(b => b.Book)
               .ToListAsync();

            return resultBooks;
        }

        public async Task<IEnumerable<StudentBook>> GetRentedBookByIdAsync(string id)
        {
            var resultBooks = await _context.StudentBooks
              .Include(b => b.User)
              .Include(b => b.Book)
              .ToListAsync();

            return resultBooks.Where(x => x.User.UserCardIdentity == id);
        }

        public async Task<string> AddRentBookAsync(RentBookModel bookModel)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.BookId == bookModel.BookId);

            if (bookModel.Type == "Rent")
            {
                var rentBook = await _context.StudentBooks
                    .FirstOrDefaultAsync(b => b.BookId == bookModel.BookId);

                if (rentBook != null)
                {
                    return "This Book is not available to rent ";
                }
                else
                {
                    var rentbookEntity = new StudentBook
                    {
                        UserId = bookModel.UserId,
                        BookId = bookModel.BookId,
                        BorrowedDate = DateOnly.FromDateTime(DateTime.Today),
                    };

                    _context.StudentBooks.Add(rentbookEntity);
                    await _context.SaveChangesAsync();

                    return "Book Has been Rented Out";
                }
            } 
            else if (bookModel.Type == "Return")
            {
                var rentedBook = await _context.StudentBooks
                    .FirstOrDefaultAsync(b => b.BookId == bookModel.BookId);

                rentedBook!.ReturnedDate = DateOnly.FromDateTime(DateTime.Today);

                await _context.SaveChangesAsync();

                return "Book Has been Returned";
            }

            return "Invalid operation.";
        }
    }
}