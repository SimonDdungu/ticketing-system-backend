using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.Filters;
using Ticketing_backend.Models.Users;
using Ticketing_backend.Repositories.Interfaces;

namespace Ticketing_backend.Repositories.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) {}

    public async Task<User?> GetByPublicId(string publicId) => 
        await _dbSet.FirstOrDefaultAsync(u => u.PublicId == publicId);

    public async Task<PaginatedResponse<User>> GetAllAsync(UserFilterRequest filter)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.FirstName))
            query = query.Where(u => u.FirstName.Contains(filter.FirstName));

        if (!string.IsNullOrWhiteSpace(filter.LastName))
            query = query.Where(u => u.LastName.Contains(filter.LastName));

        if (!string.IsNullOrWhiteSpace(filter.Email))
            query = query.Where(u => u.Email != null && u.Email.Contains(filter.Email));

        if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
            query = query.Where(u => u.PhoneNumber != null && u.PhoneNumber.Contains(filter.PhoneNumber));

        if (!string.IsNullOrWhiteSpace(filter.PublicId))
            query = query.Where(u => u.PublicId == filter.PublicId);

        if (filter.IsBanned.HasValue)
            query = query.Where(u => u.IsBanned == filter.IsBanned.Value);
        else
            query = query.Where(u => !u.IsBanned);

        if (filter.IsDeleted.HasValue)
            query = query.Where(u => u.IsDeleted == filter.IsDeleted.Value);
        

        var totalCount = await query.CountAsync();

        var users = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PaginatedResponse<User>
        {
            Data = users,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<IEnumerable<User>> GetByFirstName(string firstName) =>
        await _dbSet.Where(u => u.FirstName.Contains(firstName)).ToListAsync();

    public async Task<IEnumerable<User>> GetByLastName(string lastName) =>
        await _dbSet.Where(u => u.LastName.Contains(lastName)).ToListAsync();

    public async Task<User?> GetByEmail(string email) => 
        await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByPhoneNumber(string phoneNumber) => 
        await _dbSet.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
}