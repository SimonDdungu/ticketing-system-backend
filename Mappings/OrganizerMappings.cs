using Ticketing_backend.DTOs.Organizer;
using Ticketing_backend.Models.Organizers;

namespace Ticketing_backend.Mappings;

public static class OrganizerMappings
{
    public static OrganizerResponse ToResponse(this Organizer organizer) => new()
    {
        Id = organizer.Id,
        Name = organizer.Name,
        Email = organizer.Email,
        PhoneNumber = organizer.PhoneNumber,
        Bio = organizer.Bio,
        LogoUrl = organizer.LogoUrl,
        CreatedAt = organizer.CreatedAt,
        UpdatedAt = organizer.UpdatedAt
    };

    public static Organizer ToModel(this CreateOrganizerRequest request) => new()
    {
        Name = request.Name,
        Email = request.Email,
        PhoneNumber = request.PhoneNumber,
        Bio = request.Bio,
        LogoUrl = request.LogoUrl
    };

    public static void UpdateModel(this Organizer organizer, UpdateOrganizerRequest request)
    {
        if (request.Name is not null) organizer.Name = request.Name;
        if (request.Email is not null) organizer.Email = request.Email;
        if (request.PhoneNumber is not null) organizer.PhoneNumber = request.PhoneNumber;
        if (request.Bio is not null) organizer.Bio = request.Bio;
        if (request.LogoUrl is not null) organizer.LogoUrl = request.LogoUrl;
    }
}