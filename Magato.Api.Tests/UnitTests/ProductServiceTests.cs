
using FluentAssertions;
using Moq;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Services;

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
        var dto = new ProductDto { Title = "Shirt", Price = 199 };
        _service.Add(dto);

        _repoMock.Verify(r => r.Add(It.Is<Product>(p => p.Title == "Shirt" && p.Price == 199)), Times.Once);
    }

    [Fact]
    public void Delete_Calls_Repo_Delete()
    {
        _service.Delete(3);
        _repoMock.Verify(r => r.Delete(3), Times.Once);
    }
}
