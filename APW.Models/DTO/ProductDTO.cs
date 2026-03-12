using APW.Models.Entities.Productdb;

namespace APW.Models.DTO
{
    public class ProductDTO
    {
        public IEnumerable<Product> Products { get; set; } = [];

        public List<ProductSummary> Summaries { get; set; } = [];
    }
}
