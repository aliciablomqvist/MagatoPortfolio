using FluentAssertions;

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Services;

using Moq;


namespace Magato.Tests.UnitTests
{
    public class CollectionServiceTests
    {
        private readonly Mock<ICollectionRepository> _repoMock = new();
        private readonly CollectionService _service;

        public CollectionServiceTests()
        {
            _service = new CollectionService(_repoMock.Object);
        }

        [Fact]
        public async Task AddCollectionAsync_ShouldAddCollection()
        {
            var dto = new CollectionCreateDto
            {
                CollectionTitle = "Höst 2025",
                CollectionDescription = "Snygga jackor",
                ReleaseDate = DateTime.Now
            };

            await _service.AddCollectionAsync(dto);

            _repoMock.Verify(r => r.AddCollectionAsync(It.Is<Collection>(
                c => c.CollectionTitle == dto.CollectionTitle &&
                     c.CollectionDescription == dto.CollectionDescription)), Times.Once);
        }

        [Fact]
        public async Task DeleteCollectionAsync_ShouldReturnFalse_WhenNotFound()
        {
            _repoMock.Setup(r => r.CollectionExistsAsync(It.IsAny<int>())).ReturnsAsync(false);

            var result = await _service.DeleteCollectionAsync(99);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AddCollectionAsync_ShouldAddCollectionWithDetails()
        {
            // Arrange
            var dto = new CollectionCreateDto
            {
                CollectionTitle = "Sommar 2025",
                CollectionDescription = "Ljusa färger",
                ReleaseDate = DateTime.UtcNow,
                Colors = new List<ColorDto> { new ColorDto { Name = "White", Hex = "#FFFFFF" } },
                Materials = new List<MaterialDto> { new MaterialDto { Name = "Bomull", Description = "Lent!" } },
                Sketches = new List<SketchDto> { new SketchDto { Url = "http://example.com/sketch.png" } }
            };

            var mockRepo = new Mock<ICollectionRepository>();
            var service = new CollectionService(mockRepo.Object);

            await service.AddCollectionAsync(dto);
            mockRepo.Verify(r => r.AddCollectionAsync(It.Is<Collection>(c =>
                c.CollectionTitle == dto.CollectionTitle &&
                c.CollectionDescription == dto.CollectionDescription &&
                c.Colors.Count == 1 &&
                c.Colors.First().Name == "White" &&
                c.Materials.Count == 1 &&
                c.Materials.First().Name == "Bomull" &&
                c.Sketches.Count == 1 &&
                c.Sketches.First().Url == "http://example.com/sketch.png"
            )), Times.Once);
        }

        [Fact]
        public async Task UpdateCollectionAsync_ShouldReturnFalse_WhenCollectionNotFound()
        {
            var mockRepo = new Mock<ICollectionRepository>();
            mockRepo.Setup(r => r.GetCollectionByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((Collection?)null);

            var service = new CollectionService(mockRepo.Object);

            var dto = new CollectionDto { Id = 99, CollectionTitle = "Uppdaterad" };
            var result = await service.UpdateCollectionAsync(99, dto);

            result.Should().BeFalse();
        }

    }
}
