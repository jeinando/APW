using APW.Data.Foodbankdb.Models;
using APW.Data.Repositories;

namespace APW.Core.BusinessLogic;

public interface IFoodItemBusiness
{
    /// <summary>Deletes the fooditem associated with the fooditem id.</summary>
    Task<bool> DeleteFoodItemAsync(int id);

    /// <summary>Gets food items. If id is provided, returns only that fooditem; otherwise returns all food items.</summary>
    Task<IEnumerable<FoodItem>> GetFoodItems(int? id);

    /// <summary>Saves a fooditem (creates or updates).</summary>
    Task<bool> SaveFoodItemAsync(FoodItem fooditem);

    /// <summary>Searches food items by multiple optional criteria using EF Core server-side filtering.</summary>
    Task<IEnumerable<FoodItem>> SearchFoodItemsAsync(
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

public class FooditemBusiness(IRepositoryFoodItem repositoryFoodItem) : IFoodItemBusiness
{
    /// <inheritdoc />
    public async Task<bool> SaveFoodItemAsync(FoodItem fooditem)
    {
        return await repositoryFoodItem.UpdateAsync(fooditem);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteFoodItemAsync(int id)
    {
        var fooditem = await repositoryFoodItem.FindAsync(id);
        if (fooditem == null) return false;
        return await repositoryFoodItem.DeleteAsync(fooditem);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FoodItem>> GetFoodItems(int? id)
    {
        return id == null
            ? await repositoryFoodItem.ReadAsync()
            : [await repositoryFoodItem.FindAsync((int)id)];
    }

    /// <inheritdoc />
    public async Task<IEnumerable<FoodItem>> SearchFoodItemsAsync(
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
        return await repositoryFoodItem.SearchAsync(
            name, category, brand, description,
            priceMin, priceMax, unit, quantityInStock,
            expirationDateFrom, expirationDateTo, isPerishable,
            caloriesPerServing, ingredients, barcode, supplier,
            dateAddedFrom, dateAddedTo, isActive, roleId);
    }
}