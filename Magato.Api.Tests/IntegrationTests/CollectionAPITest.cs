using System.Collections.Generic;
using System.Threading.Tasks;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Controllers;
using Magato.Api.Services;
using Moq;
using Xunit;
using Magato.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;


namespace Magato.Tests.IntegrationTests
{

    public class CollectionsApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CollectionsApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateCollection_ShouldReturn201()
        {
            var dto = new
            {
                collectionTitle = "Vår 2025",
                collectionDescription = "Inspirerad av naturen",
                releaseDate = "2025-04-01T00:00:00Z"
            };

            var response = await _client.PostAsJsonAsync("/api/Collections", dto);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetAllCollections_ShouldReturn200()
        {
            var response = await _client.GetAsync("/api/Collections");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
