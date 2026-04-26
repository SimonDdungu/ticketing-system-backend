using Microsoft.AspNetCore.Mvc;

[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult NotFoundResponse(string message) =>
        NotFound(new { message });

    protected IActionResult BadRequestResponse(string message) =>
        BadRequest(new { message });

    protected IActionResult OkResponse<T>(T data) =>
        Ok(new { data });
}