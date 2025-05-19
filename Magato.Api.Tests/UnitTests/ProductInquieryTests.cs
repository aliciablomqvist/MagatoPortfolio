
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
    public async Task AddAsync_Calls_Repository_AddAsync()
    {
        var dto = new ProductInquiryCreateDto
        {
            ProductId = 1,
            Email = "mail@example.com",
            Message = "Do you have this product in size 42?"
        };


        _productRepoMock.Setup(r => r.GetByIdAsync(dto.ProductId))
            .ReturnsAsync(new Product { Id = 1, Title = "Sneakers" });

        _inquiryRepoMock.Setup(r => r.AddAsync(It.IsAny<ProductInquiry>()))
            .Returns(Task.CompletedTask);

        await _service.AddAsync(dto);

        _inquiryRepoMock.Verify(r => r.AddAsync(It.IsAny<ProductInquiry>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_Returns_Mapped_Dtos()
    {
        var mockData = new List<ProductInquiry>
{
            new ProductInquiry
{
                Id = 1,
                Product = new Product{ Title = "Sneakers" },
                Email = "mail@example.com",
                Message = "Do you have this product in size 42?",
                SentAt = System.DateTime.UtcNow
            }
        };

        _inquiryRepoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(mockData);

        var result = await _service.GetAllAsync();

        result.Should().HaveCount(1);
        result.First().ProductTitle.Should().Be("Sneakers");
    }
}
