using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing_backend.DTOs.Event;
using Ticketing_backend.Filters;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : BaseController
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] EventFilter filter)
    {
        var events = await _eventService.GetFilteredAsync(filter);

        return OkResponse(events);
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var ev = await _eventService.GetByIdAsync(id);

        return OkResponse(ev);
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateEventRequest request)
    {
        var created = await _eventService.CreateAsync(request);

        return OkResponse(created);
    }


    [HttpPatch("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, UpdateEventRequest request)
    {
        var updated = await _eventService.UpdateAsync(id, request);

        return OkResponse(updated);
    }


    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _eventService.DeleteAsync(id);

        return OkResponse("Event deleted successfully.");
    }
}