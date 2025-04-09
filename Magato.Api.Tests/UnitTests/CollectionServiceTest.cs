using System.Collections.Generic;
using System.Threading.Tasks;
using Magato.Api.DTO;
using Magato.Api.Controllers;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Services;
using Moq;
using Xunit;
using Magato.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;


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
            var dto = new CollectionDto
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
            var dto = new CollectionDto
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

            // Act
            var result = await service.AddCollectionAsync(dto);

            // Assert
            result.CollectionTitle.Should().Be("Sommar 2025");
            result.Colors.Should().ContainSingle();
            result.Materials.Should().ContainSingle();
            result.Sketches.Should().ContainSingle();
            mockRepo.Verify(r => r.AddCollectionAsync(It.IsAny<Collection>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCollectionAsync_ShouldReturnFalse_WhenCollectionNotFound()
        {
            var mockRepo = new Mock<ICollectionRepository>();
            mockRepo.Setup(r => r.GetCollectionByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync((Collection?)null);

            var service = new CollectionService(mockRepo.Object);

            var dto = new CollectionDto { CollectionTitle = "Uppdaterad" };
            var result = await service.UpdateCollectionAsync(99, dto);
            result.Should().BeFalse();
        }

    }
}