
using System.Reflection.Metadata;
using APW.Data.MSSQL;
using APW.Models;

namespace APW.Data.Repositories;

public interface IUserRoleRepository
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

public class UserRoleRepository(ProductDbContext context) : RepositoryBase<UserRole>(context), IUserRoleRepository, IDisposable
{
    private bool disposed = false;

   
    protected virtual void Dispose(bool disposing)
    {
        if(!this.disposed)
        {
            if(disposing)
            {
                context.Dispose();
            }

            
        } 
        this.disposed=true;
    }
     public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    
}