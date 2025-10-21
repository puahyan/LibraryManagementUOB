using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Model;

public class BookModel
{
    public int BookId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Author is required")]
    public string Author { get; set; }

    [Required(ErrorMessage = "Published Year is required")]
    public string PublishedYear { get; set; }
    public string Genre { get; set; }

    [Required(ErrorMessage = "RackId is required")]
    public int RackId { get; set; } 
    public int RentPeriod { get; set; }
}