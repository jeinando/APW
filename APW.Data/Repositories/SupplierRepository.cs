using APW.Data.MSSQL;
using APW.Models;

namespace APW.Data.Repositories;

public interface ISupplierRepository
{
    Task<bool> UpsertAsync(Supplier entity, bool isUpdating);
    Task<bool> CreateAsync(Supplier entity);
    Task<bool> DeleteAsync(Supplier entity);
    Task<IEnumerable<Supplier>> ReadAsync();
    Task<Supplier> FindAsync(int id);
    Task<bool> UpdateAsync(Supplier entity);
    Task<bool> UpdateManyAsync(IEnumerable<Supplier> entities);
    Task<bool> ExistsAsync(Supplier entity);
}

public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
{
    public SupplierRepository(ProductDbContext context) : base(context)
    {
    }
}
