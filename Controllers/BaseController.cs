using Microsoft.AspNetCore.Mvc;

namespace Ticketing_backend.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected bool IsSuperAdmin => User.IsInRole("SuperAdmin");

    protected bool IsAdmin => User.IsInRole("Admin");

    protected bool IsSupport => User.IsInRole("Support");

    protected bool IsOrganizer => User.IsInRole("Organizer");

    protected bool IsCustomer => User.IsInRole("Customer");

     protected bool IsStaff => IsSuperAdmin || IsAdmin || IsSupport;

    protected IActionResult NotFoundResponse(string message) =>
        NotFound(new { message });

    protected IActionResult BadRequestResponse(string message) =>
        BadRequest(new { message });

    protected IActionResult OkResponse<T>(T data) =>
        Ok(new { data });
}