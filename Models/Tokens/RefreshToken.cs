using System.ComponentModel.DataAnnotations;
using Elastic.Transport.Products.Elasticsearch;

namespace Ticketing_backend.Models.Tokens;
public class RefreshToken 
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Token is required")]
    public required string Token { get; set; }

    public DateTime Expiry { get; set; }

    public bool IsUsed { get; set; }

    public bool IsRevoked { get; set; }

    [Required(ErrorMessage = "UserId is required")]
    public required Guid UserId { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}