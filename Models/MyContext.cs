
using Microsoft.EntityFrameworkCore;
namespace TheWall.Models;

public class MyContext : DbContext
{

    public MyContext(DbContextOptions options) : base(options) { }

    // Models for Database
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    // association db
    public DbSet<Comment> Comments { get; set; }
}