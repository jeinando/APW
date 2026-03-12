using APW.Data.MSSQL;
using APW.Models.Entities.Productdb;

namespace APW.Data.Repositories;

public interface IRepositoryUserRole
{
    Task<bool> UpsertAsync(UserRole entity, bool isUpdating);
    Task<bool> CreateAsync(UserRole entity);
    Task<bool> DeleteAsync(UserRole entity);
    Task<IEnumerable<UserRole>> ReadAsync();
    Task<UserRole> FindAsync(int id);
    Task<bool> UpdateAsync(UserRole entity);
    Task<bool> UpdateManyAsync(IEnumerable<UserRole> entities);
    Task<bool> ExistsAsync(UserRole entity);
}

public class RepositoryUserRole : RepositoryBase<UserRole>, IRepositoryUserRole
{
    public RepositoryUserRole(ProductDbContext context) : base(context)
    {
    }
}

