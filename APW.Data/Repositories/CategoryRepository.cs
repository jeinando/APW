using APW.Models;
using APW.Data.MSSQL;
using Microsoft.EntityFrameworkCore;


namespace APW.Data.Repositories
{
 public interface IRepositoryCategory
{
    Task<bool> UpsertAsync(Category entity, bool isUpdating);
    Task<bool> CreateAsync(Category entity);
    Task<bool> DeleteAsync(Category entity);
    Task<IEnumerable<Category>> ReadAsync();
    Task<Category> FindAsync(int id);
    Task<bool> UpdateAsync(Category entity);
    Task<bool> UpdateManyAsync(IEnumerable<Category> entities);
    Task<bool> ExistsAsync(Category entity);
    Task<bool> CheckBeforeSavingAsync(Category entity);
    
}

   public class RepositoryCategory : RepositoryBase<Category>, IRepositoryCategory
{
    public RepositoryCategory(ProductDbContext context) : base(context)
    {
    }

    public async Task<bool> CheckBeforeSavingAsync(Category entity)
    {
        var exists = await ExistsAsync(entity);
        if (exists)
        {
            // algo mas
        }

        return await UpsertAsync(entity, exists);
    }

    public async new Task<bool> ExistsAsync(Category entity)
    {
        return await DbContext.Categories.AnyAsync(x => x.CategoryId == entity.CategoryId);
    }
}
}