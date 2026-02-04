using System.Text.Json.Serialization;

namespace APW.Web.Models
{
    public class ProductViewModel
    {
        [JsonPropertyName("productId")]
        public int Id { get; set; }

        [JsonPropertyName("productName")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("categoryId")]
        public int? CategoryId { get; set; }

        [JsonPropertyName("rating")]
        public decimal? Rating { get; set; }

        [JsonPropertyName("unitPrice")]
        public decimal Price { get; set; }

        [JsonPropertyName("unitsInStock")]
        public int Stock { get; set; }
    }
}