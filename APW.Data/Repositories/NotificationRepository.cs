
using APW.Data.MSSQL;
using APW.Models;

namespace APW.Data.Repositories;

public interface INotificationRepository
{
    Task<bool> UpsertAsync(Notification entity, bool isUpdating);
    Task<bool> CreateAsync(Notification entity);
    Task<bool> DeleteAsync(Notification entity);
    Task<IEnumerable<Notification>> ReadAsync();
    Task<Notification> FindAsync(int id);
    Task<bool> UpdateAsync(Notification entity);
    Task<bool> UpdateManyAsync(IEnumerable<Notification> entities);
    Task<bool> ExistsAsync(Notification entity);
}

public class NotificationRepository(ProductDbContext context) : RepositoryBase<Notification>(context), INotificationRepository
{
}