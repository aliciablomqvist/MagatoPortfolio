
namespace Magato.Api.Tests.IntegrationTests
{
    public class ProductIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public ProductIntegrationTests(WebApplicationFactory<Program> factory)
{
            _factory = factory.WithWebHostBuilder(builder =>
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
                });
            });

            _client = _factory.CreateClient();
        }

        private async Task SeedTestDataAsync()
{
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            db.Users.RemoveRange(db.Users);
            db.Categories.RemoveRange(db.Categories);
            db.Products.RemoveRange(db.Products);

            var category = new Category
{
                Id = 1,
                Name = "Shoes"
            };

            db.Categories.Add(category);

            db.Products.Add(new Product
{
                Title = "Sneakers",
                Price = 799,
                CategoryId = category.Id,
                Category = category,
                Description = "Running sneakers",
                Status = ProductStatus.InStock,
                ProductImages = new List<ProductImage>
{
            new ProductImage{ ImageUrl = "https://pic.com/sneakers1.jpg" }
        }
            });

            db.Users.Add(new User
{
                Username = "admin",
                PasswordHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes("admin123"))),
                IsAdmin = true
            });

            await db.SaveChangesAsync();
        }


        private async Task AuthenticateAsync()
{
            var login = new UserLoginDto
{
                Username = "admin",
                Password = "admin123"
            };

            var response = await _client.PostAsJsonAsync("/api/auth/login", login);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", body!.Token);
        }

        [Fact]
        public async Task GetAll_Returns_List()
{
            await SeedTestDataAsync();

            var response = await _client.GetAsync("/api/products");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>(new JsonSerializerOptions
{
                Converters ={ new JsonStringEnumConverter() }
            });

            products.Should().NotBeNull();
            products.Should().NotBeEmpty();

        }

        [Fact]
        public async Task Create_Product_Returns_Created()
{
            await SeedTestDataAsync();
            await AuthenticateAsync();

            var newProduct = new
{
                title = "Shoes with painted flowers",
                price = 1200,
                description = "Amazing shoes",
                categoryId = 1,
                imageUrls = new List<string>
{
                    "https://pic.com/dress1.jpg",
                    "https://pic.com/dress2.jpg"
                },
                status = "InStock"
            };

            var response = await _client.PostAsJsonAsync("/api/products", newProduct);

            var rawBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Create Product Response: " + rawBody);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var created = JsonSerializer.Deserialize<ProductDto>(rawBody, new JsonSerializerOptions
{
                PropertyNameCaseInsensitive = true,
                Converters ={ new JsonStringEnumConverter() }
            });

            created.Should().NotBeNull();
            created!.Title.Should().Be("Shoes with painted flowers");
        }
    }
}
