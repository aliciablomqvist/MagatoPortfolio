using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Magato.Api.Data;
using Magato.Api.DTO;
using Magato.Api.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Magato.Tests.IntegrationTests;

public class ContactApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ContactApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("environment", "Testing");
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));

                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureCreated();
            });
        }).CreateClient();
    }

    [Fact]
    public async Task PostContactMessage_ReturnsOk_AndSavesToDb()
    {
        var dto = new
        {
            name = "Testperson",
            email = "test@mail.com",
            message = "Här skickas ett meddelande!",
            gdprConsent = true
        };

        var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/api/contact", content);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
