using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Models.Users;
using Ticketing_backend.Models.Organizers;
using Ticketing_backend.Models.Events;
using Ticketing_backend.Models.Tickets;
using Ticketing_backend.Models.Orders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Ticketing_backend.Models.Permissions;
using Ticketing_backend.Models.Tokens;
namespace  Ticketing_backend.Data;

public class AppDbContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>

{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Organizer> Organizers => Set<Organizer>();

    public DbSet<Event> Events => Set<Event>();

    public DbSet<EventImage> EventImages => Set<EventImage>();

    public DbSet<TicketType> TicketTypes => Set<TicketType>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Permission> Permissions => Set<Permission>();

    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

    public DbSet<UserPermission> UserPermissions => Set<UserPermission>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();

        // Save the Enum as a String in the DB
        modelBuilder.Entity<Event>()
            .Property(p => p.Status)
            .HasConversion<string>();
        
        modelBuilder.Entity<Order>()
            .Property(p => p.Status)
            .HasConversion<string>();



        // Only one event image can be IsPrimary in the database
        modelBuilder.Entity<EventImage>()
            .HasIndex(i => i.IsPrimary)
            .IsUnique()
            .HasFilter("\"IsPrimary\" = true"); 
            
    }
}