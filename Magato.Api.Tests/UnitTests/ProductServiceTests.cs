
using FluentAssertions;

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Services;

using Moq;

namespace Magato.Api.Tests.UnitTests;

public class ProductServiceTests
{
    private readonly ProductService _service;
    private readonly Mock<IProductRepository> _repoMock;

    public ProductServiceTests()
    {
        _repoMock = new Mock<IProductRepository>();
        _service = new ProductService(_repoMock.Object);
    }

    [Fact]
    public void GetAll_Returns_All_Products()
    {
        var products = new List<Product>
        {
            new Product { Id = 1, Title = "Shoe", Price = 999 },
            new Product { Id = 2, Title = "Hat", Price = 499 }
        };

        _repoMock.Setup(r => r.GetAll()).Returns(products);

        var result = _service.GetAll();

        result.Should().HaveCount(2);
    }

    [Fact]
    public void Add_Calls_Repo_Add()
    {
        var dto = new ProductDto
        {
            Title = "Shirt",
            Description = "Pretty shirt",
            Price = 199,
            CategoryId = 1,
            ImageUrls = new List<string> { "https://examplepic.com/image.jpg" },
            Status = ProductStatus.InStock
        };

        _service.Add(dto);

        _repoMock.Verify(r => r.Add(It.Is<Product>(p =>
            p.Title == dto.Title &&
            p.Description == dto.Description &&
            p.Price == dto.Price &&
            p.CategoryId == dto.CategoryId &&
            p.Status == dto.Status &&
            p.ProductImages.Any(img => img.ImageUrl == "https://examplepic.com/image.jpg")
        )), Times.Once);
    }


    [Fact]
    public void Delete_Calls_Repo_Delete()
    {
        _service.Delete(3);
        _repoMock.Verify(r => r.Delete(3), Times.Once);
    }
}
