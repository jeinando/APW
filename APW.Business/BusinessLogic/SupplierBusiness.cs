

using APW.Data.Repositories;
using APW.Models;

namespace APW.Business.BusinessLogic;
public interface ISupplierBusiness
{
    /// <summary>
    /// Deletes the supplier associated with the supplier id.
    /// </summary>
    /// <param name="id">The supplier id.</param>
    /// <returns>True if deletion was successful, false otherwise.</returns>
    Task<bool> DeleteSupplierAsync(int id);

    /// <summary>
    /// Gets suppliers. If id is provided, returns only that supplier; otherwise returns all suppliers.
    /// </summary>
    /// <param name="id">Optional supplier id.</param>
    /// <returns>A collection of suppliers.</returns>
    Task<IEnumerable<Supplier>> GetSuppliers(int? id);

    /// <summary>
    /// Saves a supplier (creates or updates).
    /// </summary>
    /// <param name="supplier">The supplier to save.</param>
    /// <returns>True if save was successful, false otherwise.</returns>
    Task<bool> SaveSupplierAsync(Supplier supplier);
}

public class SupplierBusiness(ISupplierRepository supplierRepository) : ISupplierBusiness
{
    /// <inheritdoc />
    public async Task<bool> SaveSupplierAsync(Supplier supplier)
    {
        return await supplierRepository.UpdateAsync(supplier);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteSupplierAsync(int id)
    {
        var supplier = await supplierRepository.FindAsync(id);
        if (supplier == null) return false;
        return await supplierRepository.DeleteAsync(supplier);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Supplier>> GetSuppliers(int? id)
    {
        return id == null
            ? await supplierRepository.ReadAsync()
            : [await supplierRepository.FindAsync((int)id)];
    }
}

