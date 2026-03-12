using APW.Models.Entities.Productdb;

namespace APW.Core.BusinessLogic;

public class ProductBuilder
{
    private readonly Product _product;

    public ProductBuilder(Product product)
    {
        _product = product;
    }

    /// <summary>
    /// Paso 1: Si Rating es null, asigna 3 como valor por defecto.
    /// </summary>
    public ProductBuilder WithDefaultRating()
    {
        _product.Rating ??= 3;
        return this;
    }

    /// <summary>
    /// Paso 2: Asigna RatingClass segun las reglas de negocio.
    /// menor a 2  → D
    /// 2 a 3.5    → C
    /// 3.6 a 4.5  → B
    /// 4.5 o mas  → A
    /// </summary>
    public ProductBuilder WithRatingClass()
    {
        var rating = (double)(_product.Rating ?? 3);

        _product.RatingClass = rating switch
        {
            < 2   => "D",
            < 3.5 => "C",
            < 4.5 => "B",
            _     => "A"
        };

        return this;
    }

    /// <summary>
    /// Paso 3: Asigna TimeClass segun las reglas de negocio.
    /// 1–15      → A
    /// 15–25     → B
    /// mas de 25 → C
    /// </summary>
    public ProductBuilder WithTimeClass()
    {
        _product.TimeClass = _product.Time switch
        {
            <= 15 => "A",
            <= 25 => "B",
            _     => "C"
        };

        return this;
    }

    /// <summary>
    /// Retorna el producto con todas las reglas aplicadas.
    /// </summary>
    public Product Build() => _product;
}