// Data/RoleSeeder.cs
using Microsoft.AspNetCore.Identity;
using Ticketing_backend.Models.Permissions;
namespace Ticketing_backend.Data;


public static class RoleSeeder
{
    private static readonly Dictionary<string, string[]> RolePermissions = new()
    {
        ["SuperAdmin"] =
        [
            Permissions.Events.Create, Permissions.Events.View, Permissions.Events.Edit, Permissions.Events.Delete,
            Permissions.Orders.Create, Permissions.Orders.View,
            Permissions.Users.Create, Permissions.Users.View, Permissions.Users.Edit, Permissions.Users.Delete, Permissions.Users.Ban,
            Permissions.TicketTypes.Create, Permissions.TicketTypes.View, Permissions.TicketTypes.Edit, Permissions.TicketTypes.Delete,
            Permissions.Organizers.Create, Permissions.Organizers.View, Permissions.Organizers.Edit, Permissions.Organizers.Delete
        ],

        ["Admin"] =
        [
            Permissions.Events.Create, Permissions.Events.View, Permissions.Events.Edit, Permissions.Events.Delete,
            Permissions.Orders.View,
            Permissions.Users.View, Permissions.Users.Edit, Permissions.Users.Ban,
            Permissions.TicketTypes.Create, Permissions.TicketTypes.View, Permissions.TicketTypes.Edit, Permissions.TicketTypes.Delete,
            Permissions.Organizers.View, Permissions.Organizers.Edit, Permissions.Organizers.Delete
        ],

        ["Support"] =
        [
            Permissions.Events.Create, Permissions.Events.View, Permissions.Events.Edit,
            Permissions.Orders.View,
            Permissions.Users.View, Permissions.Users.Edit, Permissions.Users.Ban,
            Permissions.TicketTypes.Create, Permissions.TicketTypes.View, Permissions.TicketTypes.Edit,
            Permissions.Organizers.View
        ],
        ["Organizer"] =
        [
            Permissions.Events.Create, Permissions.Events.View, Permissions.Events.Edit, Permissions.Events.Delete,
            Permissions.TicketTypes.Create, Permissions.TicketTypes.View, Permissions.TicketTypes.Edit, Permissions.TicketTypes.Delete,
            Permissions.Organizers.View, Permissions.Organizers.Edit, Permissions.Organizers.Delete
        ],

        ["Customer"] =
        [
            Permissions.Events.View,
            Permissions.Orders.Create, Permissions.Orders.View,
            Permissions.TicketTypes.View,
            Permissions.Organizers.Create
        ]
    };

    public static async Task SeedAsync(RoleManager<IdentityRole<Guid>> roleManager, AppDbContext context)
    {
        // Seed all permissions into the database
        var allPermissions = RolePermissions.Values // gets all values in dictonary
                                .SelectMany(p => p) // flatens array of arrays into a list
                                .Distinct() // removes duplicates
                                .ToList(); 

        foreach (var permissionName in allPermissions)
        {
            // Checks Permission Table, return true if "Any" record matches
            if (!context.Permissions.Any(p => p.Name == permissionName))
            {
                context.Permissions.Add(new Permission
                {
                    Name = permissionName,
                    Description = Permissions.Descriptions[permissionName]
                });
            }
        }

        await context.SaveChangesAsync();





        // Seed roles and assign permissions
        foreach (var (roleName, permissions) in RolePermissions)
        {
            if (!await roleManager.RoleExistsAsync(roleName)){
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }

            var role = await roleManager.FindByNameAsync(roleName);

            foreach (var permissionName in permissions)
            {
                var permission = context.Permissions.First(p => p.Name == permissionName);

                if (!context.RolePermissions.Any(rp => rp.RoleId == role!.Id && rp.PermissionId == permission.Id))
                {
                    context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = role!.Id,
                        PermissionId = permission.Id
                    });
                }
            }
        }

        await context.SaveChangesAsync();
    }

}