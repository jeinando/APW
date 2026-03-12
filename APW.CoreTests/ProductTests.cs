using Moq;
using APW.Core.BusinessLogic;
using APW.Data.Repositories;
using APW.Models.DTO;
using APW.Architecture.Extensions;
using APW.Models.Entities.Productdb;

namespace APW.CoreTests;

public class ProductTests
{
    private readonly IEnumerable<Product> products =
    [
        new Product { ProductId = 1, ProductName = "A", Rating = 5, Time = 10 },
        new Product { ProductId = 2, ProductName = "B", Rating = 5, Time = 20 },
        new Product { ProductId = 3, ProductName = "C", Rating = 4, Time = 30 }
    ];

    private readonly List<ProductSummary> expectedSummaries =
    [
        new ProductSummary { Rating = 5, Count = 2 },
        new ProductSummary { Rating = 4, Count = 1 },
        new ProductSummary { Rating = 3, Count = 1 }
    ];

    private readonly Mock<IRepositoryProduct> _repositoryProductMock = new();
    private readonly Mock<IProductBusiness> _productBusinessMock = new();
    private readonly IProductBusiness _business;

    public ProductTests()
    {
        _repositoryProductMock = new Mock<IRepositoryProduct>();
        _productBusinessMock = new Mock<IProductBusiness>();
        _business = new ProductBusiness(_repositoryProductMock.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetProducts_WhenIdIsNull_ShouldGroupAndSummarize()
    {
      
        var fakeProductDto = new ProductDTO
        {
            Products = this.products,
            Summaries = this.expectedSummaries
        };

        _repositoryProductMock
            .Setup(rp => rp.ReadAsync())
            .ReturnsAsync(this.products);

        _productBusinessMock
            .Setup(pb => pb.GetProducts(null))
            .ReturnsAsync(fakeProductDto);

     
        var result = await _business.GetProducts(null);

        _repositoryProductMock.Verify(rp => rp.ReadAsync(), Times.Once);
        _repositoryProductMock.Verify(rp => rp.FindAsync(It.IsAny<int>()), Times.Never);
        Assert.NotNull(result);
        Assert.True(result.Products.Count() == this.products.Count());
        Assert.NotEmpty(result.Summaries);
    }

    [Fact]
    public void GetId_WhenCallingGenerateIdFromNow_ShouldRecieveGeneratedId()
    {
        
        var dateTimeNow = DateTime.Now;

      
        var generatedId = dateTimeNow.GenerateIdFromNow();

  
        Assert.True(generatedId > 0);
        Assert.IsAssignableFrom<int>(generatedId);
        Assert.InRange(generatedId, 0, int.MaxValue);
    }



    [Fact]
    public void Should_Assign_Default_Rating_When_Null()
    {
      
        var product = new Product { Rating = null, Time = 10 };

        // Act
        var result = new ProductBuilder(product)
            .WithDefaultRating()
            .Build();

        // Assert
        Assert.Equal(3, result.Rating);
    }

    [Fact]
    public void Should_Assign_RatingClass_A_When_Rating_Is_5()
    {
        // Arrange
        var product = new Product { Rating = 5, Time = 10 };

        // Act
        var result = new ProductBuilder(product)
            .WithDefaultRating()
            .WithRatingClass()
            .Build();

        // Assert
        Assert.Equal("A", result.RatingClass);
    }

    [Fact]
    public void Should_Assign_RatingClass_B_When_Rating_Is_4()
    {
        // Arrange
        var product = new Product { Rating = 4, Time = 10 };

        // Act
        var result = new ProductBuilder(product)
            .WithDefaultRating()
            .WithRatingClass()
            .Build();

        // Assert
        Assert.Equal("B", result.RatingClass);
    }

    [Fact]
    public void Should_Assign_RatingClass_C_When_Rating_Is_3()
    {
        // Arrange
        var product = new Product { Rating = 3, Time = 10 };

        // Act
        var result = new ProductBuilder(product)
            .WithDefaultRating()
            .WithRatingClass()
            .Build();

        // Assert
        Assert.Equal("C", result.RatingClass);
    }

    [Fact]
    public void Should_Assign_RatingClass_D_When_Rating_Is_1()
    {
        // Arrange
        var product = new Product { Rating = 1, Time = 10 };

        // Act
        var result = new ProductBuilder(product)
            .WithDefaultRating()
            .WithRatingClass()
            .Build();

        // Assert
        Assert.Equal("D", result.RatingClass);
    }

    [Fact]
    public void Should_Assign_TimeClass_A_When_Time_Is_10()
    {
        // Arrange
        var product = new Product { Rating = 4, Time = 10 };

        // Act
        var result = new ProductBuilder(product)
            .WithTimeClass()
            .Build();

        // Assert
        Assert.Equal("A", result.TimeClass);
    }

    [Fact]
    public void Should_Assign_TimeClass_B_When_Time_Is_20()
    {
        // Arrange
        var product = new Product { Rating = 4, Time = 20 };

        // Act
        var result = new ProductBuilder(product)
            .WithTimeClass()
            .Build();

        // Assert
        Assert.Equal("B", result.TimeClass);
    }

    [Fact]
    public void Should_Assign_TimeClass_C_When_Time_Is_30()
    {
        // Arrange
        var product = new Product { Rating = 4, Time = 30 };

        // Act
        var result = new ProductBuilder(product)
            .WithTimeClass()
            .Build();

        // Assert
        Assert.Equal("C", result.TimeClass);
    }

    [Fact]
    public void Should_Apply_All_Rules_With_Builder()
    {
       
        var product = new Product { Rating = null, Time = 20 };

   
        var result = new ProductBuilder(product)
            .WithDefaultRating()
            .WithRatingClass()
            .WithTimeClass()
            .Build();

      
        Assert.Equal(3, result.Rating);
        Assert.Equal("C", result.RatingClass);
        Assert.Equal("B", result.TimeClass);
    }
}