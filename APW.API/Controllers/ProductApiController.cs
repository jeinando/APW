using APW.Models;
using Microsoft.AspNetCore.Mvc;
using APW.Business;
using System.Diagnostics;


namespace APW.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductApiController(ILogger<ProductApiController> logger, IProductBusiness business) : ControllerBase
{
    private readonly ILogger<ProductApiController> _logger = logger;

    [HttpGet]
    public async Task<IEnumerable<Product>> Get()
    {
        _logger.LogInformation("Getting products from ProductApiController");
        var results = await business.GetProductsAsync();
        return results;
    }
}