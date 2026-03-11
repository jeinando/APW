using System.Drawing;
using APW.Architecture.Providers;
using APW.Business;
using APW.Business.BusinessLogic;
using APW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

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

    [HttpPost]
    public async Task<ComplexObject> Save([FromBody]string complex)
    {
        _logger.LogInformation("Saving products from ProductApiController");
        var product = JsonProvider.DeserializeSimple<Product>(complex);
        var result = await business.CreateProductAsync(product);
        return CreateComplexObject(result);

    }
}
