using BookLibrary.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infrastructure;

public class DatabaseContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Complaint> Complaints { get; set; }
    public DbSet<Opinion> Opinions { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}