using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Dtos;
using LibraryManagementSystem.Model;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class LibraryController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IRentService _rentService;

    public LibraryController(IBookService bookService, IRentService rentService)
    {
        _bookService = bookService;
        _rentService = rentService;
    }

    [Authorize(Roles = "Admin, Staff")]
    [HttpGet]
    public async Task<IEnumerable<BookDto>> Books()
    {
        Console.WriteLine("get books");
        return await _bookService.GetAllBooksAsync();
    }

    [Authorize(Roles = "Admin, Staff")]
    [HttpGet("{title}")]
    public async Task<ActionResult<Book>> SearchBook(string title)
    {
        var book = await _bookService.GetBookByTitleAsync(title);
        if (book == null) return NotFound();
        return Ok(book);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> AddBook([FromBody] BookModel book)
    {
        await _bookService.AddBookAsync(book);
        return CreatedAtAction(nameof(Books), new { bookId = book.BookId }, book);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBook(int id, [FromBody] BookModel book)
    {
        if (id != book.BookId) return BadRequest();
        await _bookService.UpdateBookAsync(book);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBook(int id)
    {
        await _bookService.DeleteBookAsync(id);
        return NoContent();
    }

    [Authorize(Roles = "Admin, Staff")]
    [HttpGet]
    public async Task<IEnumerable<RentBookDto>> ViewTotalRentBooksByStudents()
    {
         return await _rentService.GetAllRentedBooksAsync();
    }

    [Authorize(Roles = "Staff")]
    [HttpPost]
    public async Task<ActionResult> RentReturnBooksToStudent([FromBody] RentBookModel book)
    {
        var result = await _rentService.AddRentBookAsync(book);

        return Ok(new { result });
    }

    [Authorize(Roles = "Admin, Staff, Student")]
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> CheckStudentBookByIdentity(string id)
    {
        var studentBook = await _rentService.GetStudentBookByIdAsync(id);

        if (studentBook == null) return NotFound();
        return Ok(studentBook);
    }

    [Authorize(Roles = "Admin, Staff")]
    [HttpGet]
    public async Task<IEnumerable<RentBookDto>> RentNotificationExpiryStudentBooks()
    {
        var expireResult = await _rentService.GetAllRentedBooksAsync();
        var today = DateOnly.FromDateTime(DateTime.Today);
         
        var expiredResult = expireResult.Where(rentBook => rentBook.ReturnedDate == null && rentBook.BorrowedDate.AddDays((int)rentBook.RentPeriod) <= today);

        return expiredResult;
    }
}