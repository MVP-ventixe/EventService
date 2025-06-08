using Infrastructure.Data;
using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public interface IEventRepository
    {
        Task<RepositoryResult<IEnumerable<EventEntity>>> GetAllAsync();
        Task<RepositoryResult<EventEntity?>> GetByIdAsync(object id);
    }
}