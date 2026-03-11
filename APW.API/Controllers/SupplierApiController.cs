using System.Drawing;
using APW.Architecture.Providers;
using APW.Business;
using APW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace APW.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierApiController(
    ILogger<SupplierApiController> logger,
    ISupplierBusiness business) : ApiControllerBase
{
    private readonly ILogger<SupplierApiController> _logger = logger;

    [HttpGet]
    public async Task<ComplexObject> Get()
    {
        _logger.LogInformation("Getting Suppliers from SupplierApiController");

        var results = await business.GetSuppliersAsync();

        return CreateComplexObject(results);
    }

    [HttpPost]
    public async Task<ComplexObject> Save([FromBody]string complex)
    {
        _logger.LogInformation("Saving Suppliers from SupplierApiController");
        var Supplier = JsonProvider.DeserializeSimple<Supplier>(complex);
        var result = await business.CreateSupplierAsync(Supplier);
        return CreateComplexObject(result);

    }
}
