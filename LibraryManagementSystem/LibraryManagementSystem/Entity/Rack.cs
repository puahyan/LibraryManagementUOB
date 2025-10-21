using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Entity;

public partial class Rack
{
    public int RackId { get; set; }

    public string RackNo { get; set; } = null!;

    public int RackRowNo { get; set; }

    public int RackColumnNo { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
