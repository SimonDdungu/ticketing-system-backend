using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.Models.Permissions;
using Ticketing_backend.Repositories.Interfaces;

namespace Ticketing_backend.Repositories.Implementations;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    public PermissionRepository(AppDbContext context) : base(context) {}

    public async Task<IEnumerable<Permission>> GetByName(string name) => 
        await _dbSet.Where(p => p.Name.Contains(name)).ToListAsync();
}