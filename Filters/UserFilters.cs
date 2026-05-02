using Ticketing_backend.DTOs.Pagination;

namespace Ticketing_backend.Filters;

public class UserFilterRequest : PaginationRequest
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PublicId { get; set; } 

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsBanned { get; set; }
}