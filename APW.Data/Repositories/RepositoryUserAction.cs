using APW.Data.MSSQL;
using APW.Models.Entities.Productdb;

namespace APW.Data.Repositories;

public interface IRepositoryUserAction
{
    Task<bool> UpsertAsync(UserAction entity, bool isUpdating);
    Task<bool> CreateAsync(UserAction entity);
    Task<bool> DeleteAsync(UserAction entity);
    Task<IEnumerable<UserAction>> ReadAsync();
    Task<UserAction> FindAsync(int id);
    Task<bool> UpdateAsync(UserAction entity);
    Task<bool> UpdateManyAsync(IEnumerable<UserAction> entities);
    Task<bool> ExistsAsync(UserAction entity);
}

public class RepositoryUserAction : RepositoryBase<UserAction>, IRepositoryUserAction
{
    public RepositoryUserAction(ProductDbContext context) : base(context)
    {
    }
}

