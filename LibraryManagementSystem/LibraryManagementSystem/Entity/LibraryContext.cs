using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Entity;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Rack> Racks { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StudentBook> StudentBooks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
  new Role { Id = 1, Name = "Admin", Description = "Administrator role" },
  new Role { Id = 2, Name = "Staff", Description = "Staff role" },
  new Role { Id = 3, Name = "Student", Description = "Student role" }
);

        for (int i = 1; i <= 20; i++)
        {
            modelBuilder.Entity<Rack>().HasData(
                new Rack { RackId = i, RackNo = $"{i}", RackRowNo = i, RackColumnNo = i + 1 }
            );
        }
         
        for (int i = 1; i <= 10; i++)
        {
            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                Id = i,
                UserName = $"user{i}",
                Email = $"user{i}@example.com",
                CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0),
                RoleId = (i % 3) + 1,
                UserCardIdentity = "I" + (0600576 + i).ToString(),
                PasswordHash = "AQAAAAIAAYagAAAAEGd2qnZ02hTmP9AY2egh+/K7/HGG1y98umQNElUeL5SpQPkMrkF318OsqeF2AaZhVA=="
            };
            
            modelBuilder.Entity<User>().HasData(user);
        }
        
        for (int i = 1; i <= 50; i++)
        {
            Random random = new Random();

            modelBuilder.Entity<Book>().HasData(new Book
            {
                BookId = i,
                Title = $"Book Title {i}",
                Author = $"Author {i}",
                Genre = (i % 5 == 0) ? "Fiction" : "Non-Fiction",
                RackId = (i % 20) + 1,
                RentPeriod = (i % 20),
            });
        }
        
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207B86B18B9");

            entity.HasIndex(e => e.RackId, "IX_Books_RackId");

            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Rack).WithMany(p => p.Books)
                .HasForeignKey(d => d.RackId)
                .HasConstraintName("FK_Books_Racks");
        });

        modelBuilder.Entity<Rack>(entity =>
        {
            entity.HasKey(e => e.RackId).HasName("PK__Racks__0363DAA82632F8C1");

            entity.Property(e => e.RackNo).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07C911A344");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<StudentBook>(entity =>
        {
            entity.HasKey(e => e.StudentBookId).HasName("PK__StudentB__F916A6E4F2C0536C");

            entity.HasIndex(e => e.BookId, "IX_StudentBooks_BookId");

            entity.HasIndex(e => e.UserId, "IX_StudentBooks_UserId");

            entity.HasOne(d => d.Book).WithMany(p => p.StudentBooks)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book");

            entity.HasOne(d => d.User).WithMany(p => p.StudentBooks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Book");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07842B175D");

            entity.HasIndex(e => e.RoleId, "IX_Users_RoleId");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.UserCardIdentity)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
