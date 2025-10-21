namespace LibraryManagementSystem.Dtos;

public class BookDto
{
    public int BookId { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string Title { get; set; }
    public int? PublishedYear { get; set; }
    public int RackColumnNo { get; set; }
    public int RackRowNo { get; set; }
    public string RackNo { get; set; }
}
