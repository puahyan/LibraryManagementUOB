using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Model;

public class RentBookModel
{
    [Required(ErrorMessage = "Book Id is required")]
    public int BookId { get; set; }

    [Required(ErrorMessage = "User Id is required")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Return or Rent")]
    public string Type { get; set; }
}