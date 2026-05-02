namespace Ticketing_backend.DTOs.User;

public class BanUserRequest
{
    public required bool IsBanned { get; set; }
    public string? Reason { get; set; }
}