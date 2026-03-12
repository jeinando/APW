using Microsoft.EntityFrameworkCore;
using APW.Data.Foodbankdb.Models;
using APW.Data.MSSQL;

namespace APW.Data.Repositories;

public interface IRepositoryFoodItem
{
    Task<bool> UpsertAsync(FoodItem entity, bool isUpdating);
    Task<bool> CreateAsync(FoodItem entity);
    Task<bool> DeleteAsync(FoodItem entity);
    Task<IEnumerable<FoodItem>> ReadAsync();
    Task<FoodItem> FindAsync(int id);
    Task<bool> UpdateAsync(FoodItem entity);
    Task<bool> UpdateManyAsync(IEnumerable<FoodItem> entities);
    Task<bool> ExistsAsync(FoodItem entity);
    Task<IEnumerable<FoodItem>> SearchAsync(
        string? name,
        string? category,
        string? brand,
        string? description,
        decimal? priceMin,
        decimal? priceMax,
        string? unit,
        int? quantityInStock,
        DateOnly? expirationDateFrom,
        DateOnly? expirationDateTo,
        bool? isPerishable,
        int? caloriesPerServing,
        string? ingredients,
        string? barcode,
        string? supplier,
        DateTime? dateAddedFrom,
        DateTime? dateAddedTo,
        bool? isActive,
        int? roleId
    );
}

public class RepositoryFoodItem(FoodbankDbContext context)
    : FoodbankRepositoryBase<FoodItem>(context), IRepositoryFoodItem
{
    public async Task<IEnumerable<FoodItem>> SearchAsync(
        string? name,
        string? category,
        string? brand,
        string? description,
        decimal? priceMin,
        decimal? priceMax,
        string? unit,
        int? quantityInStock,
        DateOnly? expirationDateFrom,
        DateOnly? expirationDateTo,
        bool? isPerishable,
        int? caloriesPerServing,
        string? ingredients,
        string? barcode,
        string? supplier,
        DateTime? dateAddedFrom,
        DateTime? dateAddedTo,
        bool? isActive,
        int? roleId)
    {
        var query = DbSet.AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(x => x.Name.Contains(name));

        if (!string.IsNullOrEmpty(category))
            query = query.Where(x => x.Category != null && x.Category.Contains(category));

        if (!string.IsNullOrEmpty(brand))
            query = query.Where(x => x.Brand != null && x.Brand.Contains(brand));

        if (!string.IsNullOrEmpty(description))
            query = query.Where(x => x.Description != null && x.Description.Contains(description));

        if (priceMin.HasValue)
            query = query.Where(x => x.Price >= priceMin.Value);

        if (priceMax.HasValue)
            query = query.Where(x => x.Price <= priceMax.Value);

        if (!string.IsNullOrEmpty(unit))
            query = query.Where(x => x.Unit != null && x.Unit.Contains(unit));

        if (quantityInStock.HasValue)
            query = query.Where(x => x.QuantityInStock == quantityInStock.Value);

        if (expirationDateFrom.HasValue)
            query = query.Where(x => x.ExpirationDate.HasValue && x.ExpirationDate.Value >= expirationDateFrom.Value);

        if (expirationDateTo.HasValue)
            query = query.Where(x => x.ExpirationDate.HasValue && x.ExpirationDate.Value <= expirationDateTo.Value);

        if (isPerishable.HasValue)
            query = query.Where(x => x.IsPerishable == isPerishable.Value);

        if (caloriesPerServing.HasValue)
            query = query.Where(x => x.CaloriesPerServing == caloriesPerServing.Value);

        if (!string.IsNullOrEmpty(ingredients))
            query = query.Where(x => x.Ingredients != null && x.Ingredients.Contains(ingredients));

        if (!string.IsNullOrEmpty(barcode))
            query = query.Where(x => x.Barcode != null && x.Barcode.Contains(barcode));

        if (!string.IsNullOrEmpty(supplier))
            query = query.Where(x => x.Supplier != null && x.Supplier.Contains(supplier));

        if (dateAddedFrom.HasValue)
            query = query.Where(x => x.DateAdded.HasValue && x.DateAdded.Value >= dateAddedFrom.Value);

        if (dateAddedTo.HasValue)
            query = query.Where(x => x.DateAdded.HasValue && x.DateAdded.Value <= dateAddedTo.Value);

        if (isActive.HasValue)
            query = query.Where(x => x.IsActive == isActive.Value);

        if (roleId.HasValue)
            query = query.Where(x => x.RoleId == roleId.Value);

        return await query.AsNoTracking().ToListAsync();
    }
}