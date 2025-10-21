using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Entity;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public int? PublishedYear { get; set; }

    public string? Genre { get; set; }

    public int? RackId { get; set; }

    public int? RentPeriod { get; set; }

    public virtual Rack? Rack { get; set; }

    public virtual ICollection<StudentBook> StudentBooks { get; set; } = new List<StudentBook>();
}
