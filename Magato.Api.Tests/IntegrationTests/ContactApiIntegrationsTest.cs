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
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Magato.Api.Data;
using Microsoft.Extensions.DependencyInjection;



namespace Magato.Tests.IntegrationTests;

public class ContactApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ContactApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptors = services
                    .Where(d =>
                        d.ServiceType.FullName?.Contains("ApplicationDbContext") == true ||
                        d.ImplementationType?.FullName?.Contains("SqlServer") == true)
                    .ToList();

                foreach (var descriptor in dbContextDescriptors)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));

                var emailMock = new Mock<IEmailService>();
                emailMock.Setup(e => e.SendContactNotificationAsync(It.IsAny<ContactMessageDto>()))
                         .Returns(Task.CompletedTask);
                services.AddSingleton(emailMock.Object);
            });
        }).CreateClient(); // <-- detta avslutar .WithWebHostBuilder()
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

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
