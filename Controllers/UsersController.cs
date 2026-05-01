// Controllers/UsersController.cs
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing_backend.DTOs.User;
using Ticketing_backend.Filters;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,SuperAdmin,Support")]
    public async Task<IActionResult> GetAll([FromQuery] UserFilterRequest filter)
    {
            var users = await _userService.GetAllAsync(filter);

            return OkResponse(users);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (!IsStaff && currentUserId != id)
                return Forbid();

            var user = await _userService.GetByIdAsync(id);

            if (user is null) return NotFoundResponse($"User with id {id} not found.");

            return OkResponse(user);
    }

    [HttpPatch("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, UpdateUserRequest request)
    {
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
     

            if (!IsStaff && currentUserId != id)
                return Forbid();

            var user = await _userService.UpdateAsync(id, request);

            return OkResponse(user);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (!IsSuperAdmin && currentUserId != id)
                return Forbid();

            await _userService.DeleteAsync(id);
            
            return OkResponse("User deleted successfully.");
    }
}