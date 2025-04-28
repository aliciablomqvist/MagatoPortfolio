using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace Magato.Api.Tests.IntegrationTests;

public class ProductInquiryIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductInquiryIntegrationTests(WebApplicationFactory<Program> factory)
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
                    options.UseInMemoryDatabase("InquiryTestDb"));

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureCreated();

                db.Users.RemoveRange(db.Users);
                db.Users.Add(new User
                {
                    Username = "admin",
                    PasswordHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes("admin123"))),
                    IsAdmin = true
                });

                db.Categories.RemoveRange(db.Categories);
                var category = new Category { Id = 1, Name = "Shoes" };
                db.Categories.Add(category);

                db.Products.RemoveRange(db.Products);

                db.Products.Add(new Product
                {
                    Id = 1,
                    Title = "Sneakers",
                    Price = 799,
                    Category = category,
                    CategoryId = category.Id,
                    Description = "Test product"
                });

                db.ProductInquiries.RemoveRange(db.ProductInquiries);
                db.ProductInquiries.Add(new ProductInquiry
                {
                    Id = 1,
                    ProductId = 1,
                    Email = "existing@user.com",
                    Message = "Do you have size 42?",
                    SentAt = DateTime.UtcNow
                });

                db.SaveChanges();
            });
        }).CreateClient();
    }

    private async Task AuthenticateAsync()
    {
        var login = new UserLoginDto
        {
            Username = "admin",
            Password = "admin123"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/login", login);
        var body = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", body!.Token);
    }

    [Fact]
    public async Task Create_Inquiry_Returns_Created()
    {
        var dto = new ProductInquiryDto
        {
            ProductId = 1,
            Email = "customer@example.com",
            Message = "Is it available in black?"
        };

        var response = await _client.PostAsJsonAsync("/api/inquiries", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GetAll_Returns_List()
    {
        await AuthenticateAsync();

        var response = await _client.GetAsync("/api/inquiries");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<List<ProductInquiryResponseDto>>();
        result.Should().NotBeNull();
        result.Should().Contain(i => i.Email == "existing@user.com");
    }

    [Fact]
    public async Task Get_By_Id_Returns_Inquiry()
    {
        await AuthenticateAsync();

        var response = await _client.GetAsync("/api/inquiries/1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ProductInquiryResponseDto>();
        result!.Email.Should().Be("existing@user.com");
    }
}
