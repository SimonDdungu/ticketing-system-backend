using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.Models.Users;
using Ticketing_backend.Repositories.Interfaces;

namespace Ticketing_backend.Repositories.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) {}

    public async Task<User?> GetByPublicId(string publicId) => 
        await _dbSet.FirstOrDefaultAsync(u => u.PublicId == publicId);

    public async Task<IEnumerable<User>> GetByFirstName(string firstName) =>
        await _dbSet.Where(u =>u.FirstName.Contains(firstName)).ToListAsync();

    public async Task<IEnumerable<User>> GetByLastName(string lastName) =>
        await _dbSet.Where(u => u.LastName.Contains(lastName)).ToListAsync();

    public async Task<User?> GetByEmail(string email) => 
        await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByPhoneNumber(string phoneNumber) => 
        await _dbSet.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
}