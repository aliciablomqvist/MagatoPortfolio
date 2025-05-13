using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Services;

using Moq;

using Xunit;

namespace Magato.Api.Tests.UnitTests;

public class ProductInquiryServiceTests
{
    private readonly ProductInquiryService _service;
    private readonly Mock<IProductInquiryRepository> _inquiryRepoMock;
    private readonly Mock<IProductRepository> _productRepoMock;

    public ProductInquiryServiceTests()
    {
        _inquiryRepoMock = new Mock<IProductInquiryRepository>();
        _productRepoMock = new Mock<IProductRepository>();
        _service = new ProductInquiryService(_inquiryRepoMock.Object, _productRepoMock.Object);
    }

    [Fact]
    public void Add_Calls_Repository_Add()
    {
        var dto = new ProductInquiryDto
        {
            ProductId = 1,
            Email = "mail@example.com",
            Message = "Do you have this product in size 42?"
        };
        _service.Add(dto);

        _inquiryRepoMock.Verify(r => r.Add(It.IsAny<ProductInquiry>()), Times.Once);
    }

    [Fact]
    public void GetAll_Returns_Mapped_Dtos()
    {
        var mockData = new List<ProductInquiry>
        {
            new ProductInquiry
            {
                Product = new Product { Title = "Sneakers" },
                Email = "mail@example.com",
                Message = "Do you have this product in size 42?",
                SentAt = System.DateTime.UtcNow
            }
        };

        _inquiryRepoMock.Setup(r => r.GetAll()).Returns(mockData);

        var result = _service.GetAll();

        result.Should().HaveCount(1);
        result.First().ProductTitle.Should().Be("Sneakers");
    }
}
