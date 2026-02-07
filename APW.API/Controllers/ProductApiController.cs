namespace APW.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductApiController(ILogger<ProductApiController> logger, IProductBusiness business) : ControllerBase
    {
        private readonly ILogger<ProductApiController> _logger = logger;

        
        public async Task<IEnumerable<Product>> Get()
        {
            _logger.LogInformation("Getting products from ProductApiController");
            var results = await business.GetProductsAsync();
            return results;
        }
    }
}