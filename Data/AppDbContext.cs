using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Models;
namespace  Ticketing_backend.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<User> Users => Set<User>();
    public DbSet<Organizer> Organizers => Set<Organizer>();
}