// Controllers/OrganizerController.cs
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing_backend.DTOs.Organizer;
using Ticketing_backend.Filters;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizerController : BaseController
{
    private readonly IOrganizerService _organizerService;

    public OrganizerController(IOrganizerService organizerService)
    {
        _organizerService = organizerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] OrganizerFilter filter)
    {
        var organizers = await _organizerService.GetFilteredAsync(filter);
        
        return OkResponse(organizers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var organizer = await _organizerService.GetByIdAsync(id);

        return OkResponse(organizer);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateOrganizerRequest request)
    {
        var organizer = await _organizerService.CreateAsync(request);

        return OkResponse(organizer);
    }

    [HttpPatch("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, UpdateOrganizerRequest request)
    {
        var updated = await _organizerService.UpdateAsync(id, request);

        return OkResponse(updated);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _organizerService.DeleteAsync(id);

        return OkResponse("Organizer deleted successfully.");
    }
}