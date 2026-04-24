using Ticketing_backend.Models.Permissions;

namespace Ticketing_backend.Repositories.Interfaces;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<IEnumerable<Permission>> GetByName(string name);
}