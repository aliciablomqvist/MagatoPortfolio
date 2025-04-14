
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Magato.Api.DTO;
using Magato.Api.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Magato.Api.Models;

namespace Magato.Api.Tests.IntegrationTests;

public class ProductIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("environment", "Testing");
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ProductTestDb"));

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureCreated();

                db.Users.RemoveRange(db.Users);
                db.Products.RemoveRange(db.Products);

                db.Users.Add(new User
                {
                    Username = "admin",
                    PasswordHash = Convert.ToBase64String(
                        System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes("admin123"))),
                    IsAdmin = true
                });

                db.Products.Add(new Product
                {
                    Id = 1,
                    Title = "Sneakers",
                    Price = 799,
                    Category = "Shoes",
                    Description = "Comfortable running sneakers"
                });

                db.SaveChanges();
            });
        }).CreateClient();
    }

    private async Task<string> LoginAndGetTokenAsync()
    {
        var loginDto = new UserLoginDto { Username = "admin", Password = "admin123" };
        var response = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
        var content = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        return content!.Token;
    }

    [Fact]
    public async Task GetAll_Returns_List()
    {
        var response = await _client.GetAsync("/api/products");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        products.Should().NotBeNull();
        products.Should().Contain(p => p.Title == "Sneakers");
    }

    [Fact]
    public async Task Create_Product_Returns_Created()
    {
        var token = await LoginAndGetTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var dto = new ProductDto
        {
            Id = 2,
            Title = "Flower dress",
            Price = 1200,
            Category = "Dresses",
            Description = "Cute flower dress"
        };

        var response = await _client.PostAsJsonAsync("/api/products", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var created = await response.Content.ReadFromJsonAsync<ProductDto>();
        created.Should().NotBeNull();
        created!.Title.Should().Be(dto.Title);
    }
}
