
namespace Magato.Api.Tests.UnitTests
{
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
        public async Task GetAllAsync_Returns_All_Products()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Title = "Shoe", Price = 999 },
                new Product { Id = 2, Title = "Hat", Price = 499 }
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            var result = await _service.GetAllAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task AddAsync_Calls_Repo_AddAsync()
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

            await _service.AddAsync(dto);

            _repoMock.Verify(r => r.AddAsync(It.Is<Product>(p =>
                p.Title == dto.Title &&
                p.Description == dto.Description &&
                p.Price == dto.Price &&
                p.CategoryId == dto.CategoryId &&
                p.Status == dto.Status &&
                p.ProductImages.Any(img => img.ImageUrl == "https://examplepic.com/image.jpg")
            )), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Calls_Repo_DeleteAsync()
        {
            await _service.DeleteAsync(3);
            _repoMock.Verify(r => r.DeleteAsync(3), Times.Once);
        }
    }
}
