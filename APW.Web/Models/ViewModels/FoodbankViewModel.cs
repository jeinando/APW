namespace APW.Web.Models.ViewModels;

public class FoodbankViewModel
{
    public IEnumerable<FoodbankViewModel> FoodItems { get; set; } = [];

    public int FoodItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public string? Brand { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Unit { get; set; }
    public int? QuantityInStock { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public bool? IsPerishable { get; set; }
    public int? CaloriesPerServing { get; set; }
    public string? Ingredients { get; set; }
    public string? Barcode { get; set; }
    public string? Supplier { get; set; }
    public DateTime? DateAdded { get; set; }
    public bool? IsActive { get; set; }
    public int? RoleId { get; set; }

    public string? SearchName { get; set; }
    public string? SearchCategory { get; set; }
    public string? SearchBrand { get; set; }
    public string? SearchDescription { get; set; }
    public decimal? SearchPriceMin { get; set; }
    public decimal? SearchPriceMax { get; set; }
    public string? SearchUnit { get; set; }
    public int? SearchQuantityInStock { get; set; }
    public DateOnly? SearchExpirationDateFrom { get; set; }
    public DateOnly? SearchExpirationDateTo { get; set; }
    public bool? SearchIsPerishable { get; set; }
    public int? SearchCaloriesPerServing { get; set; }
    public string? SearchIngredients { get; set; }
    public string? SearchBarcode { get; set; }
    public string? SearchSupplier { get; set; }
    public DateTime? SearchDateAddedFrom { get; set; }
    public DateTime? SearchDateAddedTo { get; set; }
    public bool? SearchIsActive { get; set; }
    public int? SearchRoleId { get; set; }
}