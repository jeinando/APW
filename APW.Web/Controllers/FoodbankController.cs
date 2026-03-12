using Microsoft.AspNetCore.Mvc;
using APW.Architecture;
using APW.Architecture.Providers;
using APW.Web.Filters;
using APW.Web.Models.ViewModels;

namespace APW.Web.Controllers;

[RequireLogin]
public class FoodbankController : Controller
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public FoodbankController(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
    }

    public async Task<IActionResult> Index(FoodbankViewModel search)
    {
        try
        {
            string endpoint;
            var hasSearchParams = HasAnySearchParam(search);

            if (hasSearchParams)
            {
              
                var queryParams = BuildQueryString(search);
                endpoint = $"{_apiBaseUrl}/FoodItemApi/search?{queryParams}";
            }
            else
            {
                endpoint = $"{_apiBaseUrl}/FoodItemApi";
            }

            var response = await _restProvider.GetAsync(endpoint, null);
            var items = JsonProvider.DeserializeSimple<List<FoodbankViewModel>>(response) ?? [];

            var model = new FoodbankViewModel { FoodItems = items };
            PreserveSearchValues(search, model);

            return View(model);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading the food items: {ex.Message}";
            return View(new FoodbankViewModel());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/FoodItemApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var item = JsonProvider.DeserializeSimple<FoodbankViewModel>(response);
            if (item == null)
                return NotFound();
            return View(item);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading the food item: {ex.Message}";
            return NotFound();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FoodbankViewModel item)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/FoodItemApi";
                var json = JsonProvider.Serialize(item);
                await _restProvider.PostAsync(endpoint, json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating the food item: {ex.Message}");
        }
        return View(item);
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/FoodItemApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var item = JsonProvider.DeserializeSimple<FoodbankViewModel>(response);
            if (item == null)
                return NotFound();
            return View(item);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading the food item: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, FoodbankViewModel item)
    {
        try
        {
            if (id != item.FoodItemId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/FoodItemApi/{id}";
                var json = JsonProvider.Serialize(item);
                await _restProvider.PutAsync(endpoint, id.ToString(), json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating food item: {ex.Message}");
        }
        return View(item);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/FoodItemApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var item = JsonProvider.DeserializeSimple<FoodbankViewModel>(response);
            if (item == null)
                return NotFound();
            return View(item);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading the food item: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/FoodItemApi";
            await _restProvider.DeleteAsync(endpoint, id.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error deleting the food item: {ex.Message}";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }



    private static bool HasAnySearchParam(FoodbankViewModel s) =>
        !string.IsNullOrEmpty(s.SearchName) ||
        !string.IsNullOrEmpty(s.SearchCategory) ||
        !string.IsNullOrEmpty(s.SearchBrand) ||
        !string.IsNullOrEmpty(s.SearchDescription) ||
        s.SearchPriceMin.HasValue ||
        s.SearchPriceMax.HasValue ||
        !string.IsNullOrEmpty(s.SearchUnit) ||
        s.SearchQuantityInStock.HasValue ||
        s.SearchExpirationDateFrom.HasValue ||
        s.SearchExpirationDateTo.HasValue ||
        s.SearchIsPerishable.HasValue ||
        s.SearchCaloriesPerServing.HasValue ||
        !string.IsNullOrEmpty(s.SearchIngredients) ||
        !string.IsNullOrEmpty(s.SearchBarcode) ||
        !string.IsNullOrEmpty(s.SearchSupplier) ||
        s.SearchDateAddedFrom.HasValue ||
        s.SearchDateAddedTo.HasValue ||
        s.SearchIsActive.HasValue ||
        s.SearchRoleId.HasValue;

    private static string BuildQueryString(FoodbankViewModel s)
    {
        var parts = new List<string>();

        if (!string.IsNullOrEmpty(s.SearchName))
            parts.Add($"name={Uri.EscapeDataString(s.SearchName)}");
        if (!string.IsNullOrEmpty(s.SearchCategory))
            parts.Add($"category={Uri.EscapeDataString(s.SearchCategory)}");
        if (!string.IsNullOrEmpty(s.SearchBrand))
            parts.Add($"brand={Uri.EscapeDataString(s.SearchBrand)}");
        if (!string.IsNullOrEmpty(s.SearchDescription))
            parts.Add($"description={Uri.EscapeDataString(s.SearchDescription)}");
        if (s.SearchPriceMin.HasValue)
            parts.Add($"priceMin={s.SearchPriceMin.Value}");
        if (s.SearchPriceMax.HasValue)
            parts.Add($"priceMax={s.SearchPriceMax.Value}");
        if (!string.IsNullOrEmpty(s.SearchUnit))
            parts.Add($"unit={Uri.EscapeDataString(s.SearchUnit)}");
        if (s.SearchQuantityInStock.HasValue)
            parts.Add($"quantityInStock={s.SearchQuantityInStock.Value}");
        if (s.SearchExpirationDateFrom.HasValue)
            parts.Add($"expirationDateFrom={s.SearchExpirationDateFrom.Value:yyyy-MM-dd}");
        if (s.SearchExpirationDateTo.HasValue)
            parts.Add($"expirationDateTo={s.SearchExpirationDateTo.Value:yyyy-MM-dd}");
        if (s.SearchIsPerishable.HasValue)
            parts.Add($"isPerishable={s.SearchIsPerishable.Value}");
        if (s.SearchCaloriesPerServing.HasValue)
            parts.Add($"caloriesPerServing={s.SearchCaloriesPerServing.Value}");
        if (!string.IsNullOrEmpty(s.SearchIngredients))
            parts.Add($"ingredients={Uri.EscapeDataString(s.SearchIngredients)}");
        if (!string.IsNullOrEmpty(s.SearchBarcode))
            parts.Add($"barcode={Uri.EscapeDataString(s.SearchBarcode)}");
        if (!string.IsNullOrEmpty(s.SearchSupplier))
            parts.Add($"supplier={Uri.EscapeDataString(s.SearchSupplier)}");
        if (s.SearchDateAddedFrom.HasValue)
            parts.Add($"dateAddedFrom={s.SearchDateAddedFrom.Value:yyyy-MM-ddTHH:mm:ss}");
        if (s.SearchDateAddedTo.HasValue)
            parts.Add($"dateAddedTo={s.SearchDateAddedTo.Value:yyyy-MM-ddTHH:mm:ss}");
        if (s.SearchIsActive.HasValue)
            parts.Add($"isActive={s.SearchIsActive.Value}");
        if (s.SearchRoleId.HasValue)
            parts.Add($"roleId={s.SearchRoleId.Value}");

        return string.Join("&", parts);
    }

    private static void PreserveSearchValues(FoodbankViewModel source, FoodbankViewModel target)
    {
        target.SearchName = source.SearchName;
        target.SearchCategory = source.SearchCategory;
        target.SearchBrand = source.SearchBrand;
        target.SearchDescription = source.SearchDescription;
        target.SearchPriceMin = source.SearchPriceMin;
        target.SearchPriceMax = source.SearchPriceMax;
        target.SearchUnit = source.SearchUnit;
        target.SearchQuantityInStock = source.SearchQuantityInStock;
        target.SearchExpirationDateFrom = source.SearchExpirationDateFrom;
        target.SearchExpirationDateTo = source.SearchExpirationDateTo;
        target.SearchIsPerishable = source.SearchIsPerishable;
        target.SearchCaloriesPerServing = source.SearchCaloriesPerServing;
        target.SearchIngredients = source.SearchIngredients;
        target.SearchBarcode = source.SearchBarcode;
        target.SearchSupplier = source.SearchSupplier;
        target.SearchDateAddedFrom = source.SearchDateAddedFrom;
        target.SearchDateAddedTo = source.SearchDateAddedTo;
        target.SearchIsActive = source.SearchIsActive;
        target.SearchRoleId = source.SearchRoleId;
    }
}