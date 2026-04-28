using Ticketing_backend.DTOs.Auth;
using Ticketing_backend.DTOs.User;

namespace Ticketing_backend.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(CreateUserRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);
    Task RevokeTokenAsync(string refreshToken);
}