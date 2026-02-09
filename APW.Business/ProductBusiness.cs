using APW.Models;
using APW.Data.Repositories;

namespace APW.Business;

public interface IProductBusiness
{
    Task<bool> CreateProductAsync(Product product);
    Task<IEnumerable<Product>> GetProductsAsync();
}

public class ProductBusiness(IProductRepository productRepository) : IProductBusiness
{
    public async Task<bool> CreateProductAsync(Product product)
    {
        // service to validate the CC has funds
        // business logic here
        return await productRepository.CreateAsync(product);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        // Asynchronously retrieves a collection of products.
        return await productRepository.ReadAsync();
    }
}


