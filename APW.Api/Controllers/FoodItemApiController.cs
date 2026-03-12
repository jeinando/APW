using Microsoft.AspNetCore.Mvc;
using APW.Core.BusinessLogic;
using APW.Data.Foodbankdb.Models;

namespace APW.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodItemApiController(IFoodItemBusiness foodItemBusiness) : ControllerBase
{
    // GET: api/FoodItemApi
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FoodItem>>> Get()
    {
        var items = await foodItemBusiness.GetFoodItems(id: null);
        return Ok(items);
    }

    // GET api/FoodItemApi/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FoodItem>> Get(int id)
    {
        var items = await foodItemBusiness.GetFoodItems(id);
        var foodItem = items.FirstOrDefault();
        if (foodItem == null)
            return NotFound();
        return Ok(foodItem);
    }


    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<FoodItem>>> Search(
        [FromQuery] string? name,
        [FromQuery] string? category,
        [FromQuery] string? brand,
        [FromQuery] string? description,
        [FromQuery] decimal? priceMin,
        [FromQuery] decimal? priceMax,
        [FromQuery] string? unit,
        [FromQuery] int? quantityInStock,
        [FromQuery] DateOnly? expirationDateFrom,
        [FromQuery] DateOnly? expirationDateTo,
        [FromQuery] bool? isPerishable,
        [FromQuery] int? caloriesPerServing,
        [FromQuery] string? ingredients,
        [FromQuery] string? barcode,
        [FromQuery] string? supplier,
        [FromQuery] DateTime? dateAddedFrom,
        [FromQuery] DateTime? dateAddedTo,
        [FromQuery] bool? isActive,
        [FromQuery] int? roleId)
    {
        var items = await foodItemBusiness.SearchFoodItemsAsync(
            name, category, brand, description,
            priceMin, priceMax, unit, quantityInStock,
            expirationDateFrom, expirationDateTo, isPerishable,
            caloriesPerServing, ingredients, barcode, supplier,
            dateAddedFrom, dateAddedTo, isActive, roleId);

        return Ok(items);
    }

    // POST api/FoodItemApi
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] FoodItem foodItem)
    {
        var result = await foodItemBusiness.SaveFoodItemAsync(foodItem);
        if (result)
            return CreatedAtAction(nameof(Get), new { id = foodItem.FoodItemId }, foodItem);
        return BadRequest();
    }

    // PUT api/FoodItemApi/5
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Put(int id, [FromBody] FoodItem foodItem)
    {
        if (id != foodItem.FoodItemId)
            return BadRequest();
        var result = await foodItemBusiness.SaveFoodItemAsync(foodItem);
        if (result)
            return Ok(result);
        return NotFound();
    }

    // DELETE api/FoodItemApi/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await foodItemBusiness.DeleteFoodItemAsync(id);
        if (result)
            return Ok(result);
        return NotFound();
    }
}