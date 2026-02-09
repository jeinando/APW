
using APW.Data.MSSQL;
using APW.Models;

namespace APW.Data.Repositories;

public interface ITaskRepository
{
    Task<bool> UpsertAsync(APW.Models.Task entity, bool isUpdating);
    Task<bool> CreateAsync(APW.Models.Task entity);
    Task<bool> DeleteAsync(APW.Models.Task entity);
    Task<IEnumerable<APW.Models.Task>> ReadAsync();
    Task<APW.Models.Task> FindAsync(int id);
    Task<bool> UpdateAsync(APW.Models.Task entity);
    Task<bool> UpdateManyAsync(IEnumerable<APW.Models.Task> entities);
    Task<bool> ExistsAsync(APW.Models.Task entity);
}

public class TaskRepository(ProductDbContext context) : RepositoryBase<APW.Models.Task>(context), ITaskRepository
{
}