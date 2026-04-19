using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.Models.Organizers;
using Ticketing_backend.Repositories.Interfaces;
namespace Ticketing_backend.Repositories;

public class OrganizerRepository : Repository<Organizer>, IOrganizerRepository
{
    public OrganizerRepository(AppDbContext context) : base(context) { }

    public async Task<Organizer?> GetByUserIdAsync(Guid userId) =>
        await _dbSet.FirstOrDefaultAsync(o => o.UserId == userId);

    public async Task<Organizer?> GetByEmailAsync(string email) =>
        await _dbSet.FirstOrDefaultAsync(o => o.Email == email);

    public async Task<Organizer?> GetByPhoneNumberAsync(string phoneNumber) =>
        await _dbSet.FirstOrDefaultAsync(o => o.PhoneNumber == phoneNumber);

    public async Task<Organizer?> GetWithEventsAsync(Guid id) =>
        await _dbSet.Include(o => o.Events).FirstOrDefaultAsync(o => o.Id == id);
}