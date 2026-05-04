using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.Models.Events;

namespace Ticketing_backend.Filters;

public class EventFilter : PaginationRequest
{
    public string? Title {get; set;}
    public string? Venue {get; set;}
    public DateTime? Start {get; set;}
    public DateTime? End {get; set;}
    public EventStatus? Status {get; set;}
    public bool? IsDeleted { get; set; }
}