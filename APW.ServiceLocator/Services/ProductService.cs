using APW.Architecture;
using APW.Architecture.Providers;
using APW.Models.Entities.Productdb;
using APW.ServiceLocator.Extensions;
using APW.ServiceLocator.Services.Contracts;

namespace APW.ServiceLocator.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetDataAsync();
}

public class ProductService(IRestProvider restProvider, IConfiguration configuration) : IService<Product>, IProductService
{
    public async Task<IEnumerable<Product>> GetDataAsync()
    {
        var url = configuration.GetStringFromAppSettings("APIS", "Product");
        var response = await restProvider.GetAsync(url, null);
        return JsonProvider.DeserializeSimple<IEnumerable<Product>>(response) ?? Enumerable.Empty<Product>();
    }

}
