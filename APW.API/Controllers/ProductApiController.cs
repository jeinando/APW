using APW.Business;
using APW.Models;
using Microsoft.AspNetCore.Mvc;

namespace APW.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductApiController(
    ILogger<ProductApiController> logger,
    IProductBusiness business) : ApiControllerBase
{
    private readonly ILogger<ProductApiController> _logger = logger;

    [HttpGet]
    public async Task<ComplexObject> Get()
    {
        _logger.LogInformation("Getting products from ProductApiController");

        var results = await business.GetProductsAsync();

        return CreateComplexObject(results);
    }
}
