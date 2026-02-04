using APW.Data.Models;
//using APW.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APW.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductApiController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductApiController(ProductDbContext context)
        {
            _context = context;
        }

       [HttpGet(Name = "GetProducts")]
public async Task<IActionResult> Get()
{
    var products = await _context.Products
        .Include(p => p.Inventory) 
        .Select(p => new {
            productId = p.ProductId,
            productName = p.ProductName,
            description = p.Description,
            categoryId = p.CategoryId,
            rating = p.Rating,
          
            unitPrice = p.Inventory != null ? p.Inventory.UnitPrice : 0,
            unitsInStock = p.Inventory != null ? p.Inventory.UnitsInStock : 0
        })
        .ToListAsync();

    return Ok(products);
}
    }
}
