namespace Ticketing_backend.Documents.Event;

public class EventSearchDocument
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string Venue { get; set; } = string.Empty;
    
    public string OrganizerName { get; set; } = string.Empty;
    
    public DateTime Start { get; set; }

    public DateTime End { get; set; }
}