// DTOs/Auth/RefreshTokenRequest.cs
using System.ComponentModel.DataAnnotations;

namespace Ticketing_backend.DTOs.Auth;

public class RefreshTokenRequest
{
    [Required(ErrorMessage = "Refresh token is required.")]
    public required string RefreshToken { get; set; }
}