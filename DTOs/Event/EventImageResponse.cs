namespace Ticketing_backend.DTOs.Event;

public class EventImageResponse
{
    public Guid Id { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public bool IsPrimary { get; set; }
}