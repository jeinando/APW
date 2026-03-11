using System.Drawing;
using APW.Architecture.Providers;
using APW.Business;
using APW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace APW.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleApiController(
    ILogger<RoleApiController> logger,
    IRoleBusiness business) : ApiControllerBase
{
    private readonly ILogger<RoleApiController> _logger = logger;

    [HttpGet]
    public async Task<ComplexObject> Get()
    {
        _logger.LogInformation("Getting Roles from RoleApiController");

        var results = await business.GetRolesAsync();

        return CreateComplexObject(results);
    }

    [HttpPost]
    public async Task<ComplexObject> Save([FromBody]string complex)
    {
        _logger.LogInformation("Saving Roles from RoleApiController");
        var Role = JsonProvider.DeserializeSimple<Role>(complex);
        var result = await business.CreateRoleAsync(Role);
        return CreateComplexObject(result);

    }
}
