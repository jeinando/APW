

using APW.Data.Repositories;
using APW.Models;

namespace APW.Business.BusinessLogic;
public interface ICategoryBusiness
{
    /// <summary>
    /// Deletes the category associated with the category id.
    /// </summa
    /// ry>
    /// <param name="id">The category id.</param>
    /// <returns>True if deletion was successful, false otherwise.</returns>
    Task<bool> DeleteCategoryAsync(int id);

    /// <summary>
    /// Gets categories. If id is provided, returns only that category; otherwise returns all categories.
    /// </summary>
    /// <param name="id">Optional category id.</param>
    /// <returns>A collection of categories.</returns>
    Task<IEnumerable<Category>> GetCategories(int? id);

    /// <summary>
    /// Saves a category (creates or updates).
    /// </summary>
    /// <param name="category">The category to save.</param>
    /// <returns>True if save was successful, false otherwise.</returns>
    Task<bool> SaveCategoryAsync(Category category);
}

public class CategoryBusiness(ICategoryRepository categoryRepository) : ICategoryBusiness
{
    /// <inheritdoc />
    public async Task<bool> SaveCategoryAsync(Category category)
    {
        return await categoryRepository.UpdateAsync(category);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await categoryRepository.FindAsync(id);
        if (category == null) return false;
        return await categoryRepository.DeleteAsync(category);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Category>> GetCategories(int? id)
    {
        return id == null
            ? await categoryRepository.ReadAsync()
            : [await categoryRepository.FindAsync((int)id)];
    }
}