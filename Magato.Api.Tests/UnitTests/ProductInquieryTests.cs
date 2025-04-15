using Xunit;
using Moq;
using FluentAssertions;
using Magato.Api.Services;
using Magato.Api.Repositories;
using Magato.Api.DTO;
using Magato.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Magato.Api.Tests.UnitTests;

public class ProductInquiryServiceTests
{
    private readonly Mock<IProductInquiryRepository> _repoMock;
    private readonly ProductInquiryService _service;

    public ProductInquiryServiceTests()
    {
        _repoMock = new Mock<IProductInquiryRepository>();
        _service = new ProductInquiryService(_repoMock.Object);
    }

    [Fact]
    public void Add_Calls_Repository_Add()
    {
        var dto = new ProductInquiryDto
        {
            ProductId = 1,
            Email = "customer@example.com",
            Message = "Do you have this product in size 42?"
        };

        _service.Add(dto);

        _repoMock.Verify(r => r.Add(It.Is<ProductInquiry>(i =>
            i.ProductId == dto.ProductId &&
            i.Email == dto.Email &&
            i.Message == dto.Message
        )), Times.Once);
    }

    [Fact]
    public void GetAll_Returns_Mapped_Dtos()
    {
        var mockData = new List<ProductInquiry>
        {
            new ProductInquiry
            {
                Product = new Product { Title = "Sneakers" },
                Email = "customer@example.com",
                Message = "Do you have this product in size 42?",
                SentAt = System.DateTime.UtcNow
            }
        };

        _repoMock.Setup(r => r.GetAll()).Returns(mockData);

        var result = _service.GetAll();

        result.Should().HaveCount(1);
        result.First().ProductTitle.Should().Be("Sneakers");
    }
}
