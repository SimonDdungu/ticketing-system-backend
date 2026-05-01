using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.Filters;
using Ticketing_backend.Models.Users;

namespace Ticketing_backend.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByPublicId(string publicId);

    Task<PaginatedResponse<User>> GetAllAsync(UserFilterRequest filter);

    Task<IEnumerable<User>> GetByFirstName(string firstName);

    Task<IEnumerable<User>> GetByLastName(string lastName);

    Task<User?> GetByEmail(string email);

    Task<User?> GetByPhoneNumber(string phoneNumber);

}