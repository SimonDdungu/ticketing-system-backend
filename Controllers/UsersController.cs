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
        try
        {
            var users = await _userService.GetAllAsync(filter);
            return OkResponse(users);
        }
        catch (Exception e)
        {
            return BadRequestResponse(e.Message);
        }
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isStaff = User.IsInRole("SuperAdmin") || User.IsInRole("Admin") || User.IsInRole("Support");

            if (!isStaff && currentUserId != id)
                return Forbid();

            var user = await _userService.GetByIdAsync(id);
            if (user is null) return NotFoundResponse($"User with id {id} not found.");
            return OkResponse(user);
        }
        catch (KeyNotFoundException e)
        {
            return NotFoundResponse(e.Message);
        }
        catch (Exception e)
        {
            return BadRequestResponse(e.Message);
        }
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, UpdateUserRequest request)
    {
        try
        {
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isSuperAdmin = User.IsInRole("SuperAdmin");
            var isAdmin = User.IsInRole("Admin");
            var isSupport = User.IsInRole("Support");

            if (!isSuperAdmin && !isAdmin && !isSupport && currentUserId != id)
                return Forbid();

            var user = await _userService.UpdateAsync(id, request);
            return OkResponse(user);
        }
        catch (KeyNotFoundException e)
        {
            return NotFoundResponse(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return BadRequestResponse(e.Message);
        }
        catch (Exception e)
        {
            return BadRequestResponse(e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var isSuperAdmin = User.IsInRole("SuperAdmin");

            if (!isSuperAdmin && currentUserId != id)
                return Forbid();

            await _userService.DeleteAsync(id);
            return OkResponse("User deleted successfully.");
        }
        catch (KeyNotFoundException e)
        {
            return NotFoundResponse(e.Message);
        }
    }
}