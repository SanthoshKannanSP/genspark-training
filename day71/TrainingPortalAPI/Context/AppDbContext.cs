using Microsoft.EntityFrameworkCore;
using TrainingPortalAPI.Models;

namespace TrainingPortalAPI.Context;

public class AppDbContext : DbContext
{
    public DbSet<Video> Videos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Video>().HasKey(v => v.VideoId);
    }
}