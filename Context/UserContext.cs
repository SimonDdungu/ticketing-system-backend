using System.Security.Claims;

public class UserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor accessor)
    {
        _httpContextAccessor = accessor;
    }

    public Guid? UserId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(value, out var guid) ? guid : null;
        }
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public bool IsSuperAdmin => User?.IsInRole("SuperAdmin") ?? false;

    public bool IsAdmin => User?.IsInRole("Admin") ?? false;

    public bool IsSupport => User?.IsInRole("Support") ?? false;

    public bool IsOrganizer => User?.IsInRole("Organizer") ?? false;

    public bool IsCustomer => User?.IsInRole("Customer") ?? false;

    public bool IsAdmins => IsAdmin || IsSuperAdmin;

    public bool IsStaff => IsSuperAdmin || IsAdmin || IsSupport;
}