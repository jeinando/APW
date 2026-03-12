using APW.Architecture;
using APW.Architecture.Providers;
using APW.ServiceLocator.Extensions;

namespace APW.ServiceLocator.Services;

public class TempDataService : ITempDataService
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;

    public TempDataService(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
    }

    public async Task<IEnumerable<string>> GetDataAsync()
    {
        var url = _configuration.GetStringFromAppSettings("APIS", "TempData");
        var response = await _restProvider.GetAsync(url, null);
        return JsonProvider.DeserializeSimple<IEnumerable<string>>(response) ?? Enumerable.Empty<string>();
    }
}

public interface ITempDataService
{
    Task<IEnumerable<string>> GetDataAsync();
}
