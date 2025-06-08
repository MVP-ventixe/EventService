using Infrastructure.Data;
using Infrastructure.Models;

namespace Infrastructure.Repositories
{
    public interface IEventRepository
    {
        Task<RepositoryResult<EventEntity>> AddAsync(EventEntity entity);
        Task<RepositoryResult<EventEntity?>> GetByIdAsync(object id);
        Task<RepositoryResult<IEnumerable<EventEntity>>> GetAllAsync();
        Task<RepositoryResult<bool>> UpdateAsync(EventEntity entity);
        Task<RepositoryResult<bool>> DeleteAsync(EventEntity entity);
    }
}