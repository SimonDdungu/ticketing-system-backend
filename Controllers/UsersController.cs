using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing_backend.DTOs.SoftDelete;
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
            var user = await _userService.GetByIdAsync(id);

            return OkResponse(user);
    }

    [HttpPatch("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, UpdateUserRequest request)
    {
            var user = await _userService.UpdateAsync(id, request);

            return OkResponse(user);
    }

    [HttpPatch("{id:guid}/delete")]
    [Authorize]
    public async Task<IActionResult> SoftDelete(Guid id, SoftDeleteRequest request)
    {
            await _userService.SoftDeleteAsync(id, request);
            
            return OkResponse("User delete status updated successfully.");
    }

    [HttpDelete("{id:guid}/permanent-delete")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
            await _userService.DeleteAsync(id);
            
            return OkResponse("User permantenly deleted successfully.");
    }

    [HttpPatch("{id:guid}/ban")]
    [Authorize(Roles = "Admin,SuperAdmin,Support")]
    public async Task<IActionResult> Ban(Guid id, BanUserRequest request)
    {
        await _userService.BanAsync(id, request);

        return OkResponse("User ban status updated successfully.");
    }
}