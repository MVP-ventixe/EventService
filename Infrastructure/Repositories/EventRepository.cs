using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class EventRepository(DataContext context) : BaseRepository<EventEntity>(context), IEventRepository
{
    public override async Task<RepositoryResult<IEnumerable<EventEntity>>> GetAllAsync()
    {
        try
        {
            var list = await _dbSet.Include(x => x.EventPackages).ToListAsync();
            return new RepositoryResult<IEnumerable<EventEntity>> { IsSuccess = true, Result = list };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<EventEntity>> { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }

    public override async Task<RepositoryResult<EventEntity?>> GetByIdAsync(object id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return new RepositoryResult<EventEntity?> { IsSuccess = false, ErrorMessage = "Not found" };

            return new RepositoryResult<EventEntity?> { IsSuccess = true, Result = entity };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<EventEntity?> { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }
}
