using Microsoft.EntityFrameworkCore;
using Notify.Models;

namespace Notify.Contexts;

public class NotifyDbContext : DbContext
{
    public NotifyDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Username);
    }
}
