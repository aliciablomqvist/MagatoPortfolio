
namespace Magato.Tests.UnitTests
{
    public class CollectionWriterTests
    {
        private readonly Mock<ICollectionRepository> _repoMock = new();
        private readonly CollectionWriter _writer;

        public CollectionWriterTests()
        {
            _writer = new CollectionWriter(_repoMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddCollection()
        {
            // Arrange
            var dto = new CollectionCreateDto
            {
                CollectionTitle = "Höst 2025",
                CollectionDescription = "Snygga jackor",
                ReleaseDate = DateTime.Now
            };

            // Act
            await _writer.AddAsync(dto);

            // Assert
            _repoMock.Verify(r => r.AddAsync(It.Is<Collection>(
                c => c.CollectionTitle == dto.CollectionTitle &&
                     c.CollectionDescription == dto.CollectionDescription)), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                     .ReturnsAsync((Collection?)null);

            var result = await _writer.DeleteAsync(99);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenCollectionNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                     .ReturnsAsync((Collection?)null);

            var dto = new CollectionDto { Id = 99, CollectionTitle = "Uppdaterad" };
            var result = await _writer.UpdateAsync(99, dto);

            result.Should().BeFalse();
        }
    }
}
