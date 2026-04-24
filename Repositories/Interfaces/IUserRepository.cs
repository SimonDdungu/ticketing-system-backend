using Ticketing_backend.Models.Users;

namespace Ticketing_backend.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByPublicId(string publicId);

    Task<IEnumerable<User>> GetByFirstName(string firstName);

    Task<IEnumerable<User>> GetByLastName(string lastName);

    Task<User?> GetByEmail(string email);

}