using Microsoft.EntityFrameworkCore;
using Trevel_help.Models;

namespace Trevel_help.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<Place> Places => Set<Place>();
    public DbSet<Photo> Photos => Set<Photo>();
}
