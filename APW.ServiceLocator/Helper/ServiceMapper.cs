using APW.Models.Entities.Productdb;
using APW.ServiceLocator.Services.Contracts;
using ModelsTask = APW.Models.Entities.Productdb.Task;

namespace APW.ServiceLocator.Helper;

public interface IServiceMapper
{
    System.Threading.Tasks.Task<IService<T>> GetServiceAsync<T>(string name);
}

public class ServiceMapper : IServiceMapper
{
    private readonly IServiceProvider serviceProvider;

    public ServiceMapper(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public System.Threading.Tasks.Task<IService<T>> GetServiceAsync<T>(string name)
    {
        var service = name.ToLower() switch
        {
            "product" => (IService<T>)serviceProvider.GetRequiredService<IService<Product>>(),
            //"category" => (IService<T>)serviceProvider.GetRequiredService<IService<Category>>(),
            //"task" => (IService<T>)serviceProvider.GetRequiredService<IService<ModelsTask>>(),
            _ => throw new ArgumentException($"Service not found for '{name}'")
        };

        return System.Threading.Tasks.Task.FromResult(service);
    }
}

