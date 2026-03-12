using APW.Models.Entities.Productdb;


namespace APW.Core.Domain;

public class ProductDomain(Product product)
{
    private readonly Product _product = product;

    /// <summary>
    /// Si Rating es null, asigna 3 por defecto

    public ProductDomain CleanRating()
    {
        _product.Rating ??= 3;
        return this; // encadenar metodos
    }


    public ProductDomain ApplyRatingClass()
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
    /// Asigna TimeClass segun las reglas de negocio:
    /// 1–15      → A
    /// 15–25     → B
    /// mas de 25 → C
    /// </summary>
    public ProductDomain ApplyTimeClass()
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

    /// <summary>
    /// Metodo estatico de conveniencia para aplicar todas las reglas sobre una lista.
    
    /// </summary>
    public static IEnumerable<Product> ApplyAll(IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            yield return new ProductDomain(product)
                .CleanRating()
                .ApplyRatingClass()
                .ApplyTimeClass()
                .Build();
        }
    }
}