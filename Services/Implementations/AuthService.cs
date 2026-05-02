using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ticketing_backend.Data;
using Ticketing_backend.DTOs.Auth;
using Ticketing_backend.DTOs.User;
using Ticketing_backend.Mappings;
using Ticketing_backend.Models.Tokens;
using Ticketing_backend.Models.Users;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, AppDbContext context, IConfiguration configuration)
    {
        _userManager = userManager;
        _context = context;
        _configuration = configuration;
    }

    public async Task<AuthResponse> RegisterAsync(CreateUserRequest request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
            throw new InvalidOperationException("Email already in use.");

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, "Customer");

        return await GenerateAuthResponseAsync(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            throw new UnauthorizedAccessException("Invalid email or password.");

        if (user.IsBanned)
            throw new UnauthorizedAccessException("This account was banned.");

        if (user.IsDeleted)
        {
            user.IsDeleted = false;
            user.DeletedAt = null;
            user.UpdatedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
        }

        return await GenerateAuthResponseAsync(user);
    }

    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);

        if (refreshToken is null || refreshToken.IsRevoked || refreshToken.IsUsed)
            throw new UnauthorizedAccessException("Invalid refresh token.");

        if (refreshToken.Expiry < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token has expired.");

        // blacklist old token
        refreshToken.IsRevoked = true;
        refreshToken.IsUsed = true;
        _context.RefreshTokens.Update(refreshToken);

        var user = await _userManager.FindByIdAsync(refreshToken.UserId.ToString());
        if (user is null)
            throw new UnauthorizedAccessException("User not found.");

        return await GenerateAuthResponseAsync(user);
    }

    public async Task RevokeTokenAsync(string refreshToken)
    {
        var token = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (token is null) throw new KeyNotFoundException("Refresh token not found.");

        token.IsRevoked = true;
        _context.RefreshTokens.Update(token);
        await _context.SaveChangesAsync();
    }

    private async Task<AuthResponse> GenerateAuthResponseAsync(User user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = await GenerateRefreshTokenAsync(user.Id);

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            User = user.ToResponse()
        };
    }

    private string GenerateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Surname, user.LastName)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:AccessTokenExpiryMinutes"]!)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId)
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            UserId = userId,
            Expiry = DateTime.UtcNow.AddDays(int.Parse(_configuration["JwtSettings:RefreshTokenExpiryDays"]!)),
            IsUsed = false,
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        return refreshToken;
    }
}