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


namespace Magato.Tests.UnitTests.Services
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
    }
}