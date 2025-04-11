using System.Net.Http.Headers;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Magato.Api.Data;
using Magato.Api.DTO;
using Magato.Api.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Magato.Tests.IntegrationTests
{
    public class CollectionsApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CollectionsApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("environment", "Testing");

                builder.ConfigureServices(services =>
                {
                    var dbDescriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    if (dbDescriptor != null)
                        services.Remove(dbDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseInMemoryDatabase("TestDb"));

                    var emailMock = new Mock<IEmailService>();
                    emailMock.Setup(e => e.SendContactNotificationAsync(It.IsAny<ContactMessageDto>()))
                             .Returns(Task.CompletedTask);
                    services.AddSingleton(emailMock.Object);

                    var provider = services.BuildServiceProvider();
                    using var scope = provider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    db.Database.EnsureCreated();
                    db.Users.RemoveRange(db.Users);
                    db.SaveChanges();
                });
            }).CreateClient();
        }

        private async Task EnsureAdminExistsAsync()
        {
            var registerResponse = await _client.PostAsJsonAsync("/api/auth/register", new
            {
                username = "admin",
                password = "admin123"
            });

            if (registerResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                var body = await registerResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Register Response (400): " + body);
            }
            else
            {
                registerResponse.EnsureSuccessStatusCode();
            }
        }

        [Fact]
        public async Task Login_Should_Return_Token()
        {
            await EnsureAdminExistsAsync();

            var loginDto = new UserLoginDto { Username = "admin", Password = "admin123" };
            var loginResp = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
            var body = await loginResp.Content.ReadAsStringAsync();

            Console.WriteLine("Login status: " + loginResp.StatusCode);
            Console.WriteLine("Login response: " + body);

            loginResp.EnsureSuccessStatusCode();

            var loginResult = await loginResp.Content.ReadFromJsonAsync<LoginResponseDto>();
            loginResult.Should().NotBeNull();
            loginResult!.Token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task CreateCollection_ShouldReturn201()
        {
            await EnsureAdminExistsAsync();

            var loginDto = new UserLoginDto { Username = "admin", Password = "admin123" };
            var loginResp = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
            var loginBody = await loginResp.Content.ReadAsStringAsync();

            Console.WriteLine("Login status: " + loginResp.StatusCode);
            Console.WriteLine("Login body: " + loginBody);

            loginResp.EnsureSuccessStatusCode();

            var loginResult = await loginResp.Content.ReadFromJsonAsync<LoginResponseDto>();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult!.Token);

            var dto = new
            {
                collectionTitle = "Vår 2025",
                collectionDescription = "Inspirerad av naturen",
                releaseDate = "2025-04-01T00:00:00Z"
            };

            var response = await _client.PostAsJsonAsync("/api/Collections", dto);
            var responseText = await response.Content.ReadAsStringAsync();

            Console.WriteLine("CreateCollection status: " + response.StatusCode);
            Console.WriteLine("CreateCollection body: " + responseText);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetAllCollections_ShouldReturn200()
        {
            var response = await _client.GetAsync("/api/Collections");

            Console.WriteLine("GetAllCollections status: " + response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("GetAllCollections body: " + content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
