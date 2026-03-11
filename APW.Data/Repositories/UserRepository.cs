using APW.Data.MSSQL;
using APW.Models;

namespace APW.Data.Repositories;
public interface IUserRepository
{
    Task<bool> UpsertAsync(User entity, bool isUpdating);
    Task<bool> CreateAsync(User entity);
    Task<bool> DeleteAsync(User entity);
    Task<IEnumerable<User>> ReadAsync();
    Task<User> FindAsync(int id);
    Task<bool> UpdateAsync(User entity);
    Task<bool> UpdateManyAsync(IEnumerable<User> entities);
    Task<bool> ExistsAsync(User entity);
}

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(ProductDbContext context) : base(context)
    {
    }
}
