using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Entity;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int? RoleId { get; set; }

    public string? UserCardIdentity { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<StudentBook> StudentBooks { get; set; } = new List<StudentBook>();
}
