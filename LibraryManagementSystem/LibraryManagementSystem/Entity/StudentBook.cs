using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Entity;

public partial class StudentBook
{
    public int StudentBookId { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public DateOnly BorrowedDate { get; set; }

    public DateOnly? ReturnedDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
