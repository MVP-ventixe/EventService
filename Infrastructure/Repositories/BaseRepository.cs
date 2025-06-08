using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class BaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }



    public virtual async Task<RepositoryResult<TEntity>> AddAsync(TEntity entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<TEntity> { IsSuccess = true, Result = entity };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<TEntity> { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult<TEntity?>> GetByIdAsync(object id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return new RepositoryResult<TEntity?> { IsSuccess = false, ErrorMessage = "Not found" };

            return new RepositoryResult<TEntity?> { IsSuccess = true, Result = entity };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<TEntity?> { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
    {
        try
        {
            var list = await _dbSet.ToListAsync();
            return new RepositoryResult<IEnumerable<TEntity>> { IsSuccess = true, Result = list };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<TEntity>> { IsSuccess = false, ErrorMessage = ex.Message };
        }
    }

    public virtual async Task<RepositoryResult<bool>> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { IsSuccess = true, Result = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<bool> { IsSuccess = false, ErrorMessage = ex.Message, Result = false };
        }
    }

    public virtual async Task<RepositoryResult<bool>> DeleteAsync(TEntity entity)
    {
        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult<bool> { IsSuccess = true, Result = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<bool> { IsSuccess = false, ErrorMessage = ex.Message, Result = false };
        }
    }
}
