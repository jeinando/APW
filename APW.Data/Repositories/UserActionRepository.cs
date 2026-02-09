
using APW.Data.MSSQL;
using APW.Models;

namespace APW.Data.Repositories;

public interface IUserActionRepository
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

public class UserActionRepository(ProductDbContext context) : RepositoryBase<UserAction>(context), IUserActionRepository
{
}