using APW.Data.Repositories;
using APW.Models.DTO;
using APW.Models.Entities.Productdb;

// Nota: Reemplaza el contenido de APW.Core/BusinessLogic/ProductBusiness.cs
namespace APW.Core.BusinessLogic;

public interface IProductBusiness
{
    Task<bool> DeleteProductAsync(int id);
    Task<ProductDTO> GetProducts(int? id);
    Task<bool> SaveProductAsync(Product product);
}

public class ProductBusiness(IRepositoryProduct repositoryProduct) : IProductBusiness
{
    /// </inheritdoc>
    public async Task<bool> SaveProductAsync(Product product)
    {
        return await repositoryProduct.UpdateAsync(product);
    }

    /// </inheritdoc>
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await repositoryProduct.FindAsync(id);
        return await repositoryProduct.DeleteAsync(product);
    }

    /// </inheritdoc>
    public async Task<ProductDTO> GetProducts(int? id)
    {
        var hasId = id.HasValue;
        var productDto = new ProductDTO();

        var products = !hasId
            ? await repositoryProduct.ReadAsync()
            : [await repositoryProduct.FindAsync((int)id)];

        if (products != null && products.Any())
        {
            // Builder pattern con Fluent API
            var productsWithRules = products.Select(p =>
                new ProductBuilder(p)
                    .WithDefaultRating()
                    .WithRatingClass()
                    .WithTimeClass()
                    .Build()
            ).ToList();

            if (!hasId)
            {
                productDto.Summaries.AddRange(
                    productsWithRules
                        .GroupBy(y => y.RatingClass)
                        .SelectMany(g => g.Select(sub => new ProductSummary
                        {
                            Id = sub.ProductId,
                            Name = sub.ProductName,
                            Rating = sub.Rating,
                            Count = g.Count()
                        }))
                        .OrderByDescending(x => x.Count)
                );
            }

            productDto.Products = productsWithRules;
        }

        return productDto;
    }
}