namespace LibraryManagementSystem.Dtos;
public class RentBookDto
{
    public int BookId { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string Title { get; set; }
    public int? RentPeriod { get; set; }
    public int? PublishedYear { get; set; }
    public string UserName { get; set; }
    public string UserIdentity { get; set; }
    public DateOnly BorrowedDate { get; set; }
    public DateOnly? ReturnedDate { get; set; }
}
