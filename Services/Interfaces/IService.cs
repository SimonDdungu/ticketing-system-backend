namespace Ticketing_backend.Services.Interfaces;

public interface IService<TResponse, TCreate, TUpdate>
{
    Task<TResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<TResponse>> GetAllAsync();
    Task<TResponse> CreateAsync(TCreate request);
    Task<TResponse> UpdateAsync(Guid id, TUpdate request);
    Task DeleteAsync(Guid id);
}