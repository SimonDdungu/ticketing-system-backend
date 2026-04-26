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
    public async Task<IActionResult> GetAll([FromQuery] UserFilterRequest filter)
    {
        var users = await _userService.GetAllAsync(filter);
        return OkResponse(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user is null) return NotFoundResponse($"User with id {id} not found.");
        return OkResponse(user);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserRequest request)
    {
        try
        {
            var user = await _userService.UpdateAsync(id, request);
            return OkResponse(user);
        }
        catch (KeyNotFoundException e)
        {
            return NotFoundResponse(e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _userService.DeleteAsync(id);
            return OkResponse("User deleted successfully.");
        }
        catch (KeyNotFoundException e)
        {
            return NotFoundResponse(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return BadRequestResponse(e.Message);
        }
    }
}