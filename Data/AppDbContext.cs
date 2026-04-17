using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Models.Users;
using Ticketing_backend.Models.Organizers;
using Ticketing_backend.Models.Events;
using Ticketing_backend.Models.Tickets;
using Ticketing_backend.Models.Orders;
namespace  Ticketing_backend.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Organizer> Organizers => Set<Organizer>();

    public DbSet<Event> Events => Set<Event>();

    public DbSet<EventImage> EventImages => Set<EventImage>();

    public DbSet<TicketType> TicketTypes => Set<TicketType>();

    public DbSet<Order> Orders => Set<Order>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // This tells EF Core to save the Enum as a String in the DB
        modelBuilder.Entity<Event>()
            .Property(p => p.Status)
            .HasConversion<string>();

        modelBuilder.Entity<EventImage>()
            .HasIndex(i => i.IsPrimary)
            .IsUnique()
            .HasFilter("[IsPrimary] = 1"); // Only one image can be IsPrimary
        
        modelBuilder.Entity<Order>()
            .Property(p => p.Status)
            .HasConversion<string>();
            
    }
}