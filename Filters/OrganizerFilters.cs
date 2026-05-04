using Ticketing_backend.DTOs.Pagination;

namespace Ticketing_backend.Filters;

public class OrganizerFilter : PaginationRequest
{
    public string? Name {get; set;}

    public string? Email {get; set;} 

    public string? PhoneNumber {get; set;}

    public bool? IsDeleted { get; set; }
}