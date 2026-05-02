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
            if (!IsStaff) 
                filter.IsDeleted = false;
                filter.IsBanned = false;

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

    [HttpPatch("{id:guid}/delete")]
    [Authorize]
    public async Task<IActionResult> SoftDelete(Guid id, SoftDeleteRequest request)
    {
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (!IsStaff && currentUserId != id)
                return Forbid();

            await _userService.SoftDeleteAsync(id, request);
            
            return OkResponse("User delete status updated successfully.");
    }

    [HttpDelete("{id:guid}/permanent-delete")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            if (!IsSuperAdmin && currentUserId != id)
                return Forbid();

            await _userService.DeleteAsync(id);
            
            return OkResponse("User permantenly deleted successfully.");
    }

    [HttpPatch("{id:guid}/ban")]
    [Authorize(Roles = "Admin,SuperAdmin,Support")]
    public async Task<IActionResult> Ban(Guid id, BanUserRequest request)
    {
         if (!IsStaff)
                return Forbid();

        await _userService.BanAsync(id, request);
        return OkResponse("User ban status updated successfully.");
    }
}