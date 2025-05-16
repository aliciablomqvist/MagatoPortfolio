
namespace Magato.Api.Tests.IntegrationTests;

public class BlogIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BlogIntegrationTests(WebApplicationFactory<Program> factory)
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
                    options.UseInMemoryDatabase("BlogTestDb"));

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.EnsureCreated();

                db.Users.RemoveRange(db.Users);
                db.BlogPosts.RemoveRange(db.BlogPosts);
                db.Users.Add(new User
                {
                    Username = "admin",
                    PasswordHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes("admin123"))),
                    IsAdmin = true
                });
                db.BlogPosts.Add(new BlogPost { Id = 1, Title = "First", Content = "Hello world" });
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
        var response = await _client.GetAsync("/api/blog");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var posts = await response.Content.ReadFromJsonAsync<List<BlogPostDto>>();
        posts.Should().NotBeNull();
        posts.Should().Contain(p => p.Title == "First");
    }

    [Fact]
    public async Task Get_By_Id_Returns_Post()
    {
        var response = await _client.GetAsync("/api/blog/1");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var post = await response.Content.ReadFromJsonAsync<BlogPostDto>();
        post!.Content.Should().Be("Hello world");
    }

    [Fact]
    public async Task Create_Post_As_Admin_Returns_Created()
    {
        var token = await LoginAndGetTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var dto = new BlogPostDto
        {
            Id = 2,
            Title = "New Blog Post",
            Content = "More content",
            Author = "Tester",
            PublishedAt = DateTime.UtcNow,
            Tags = new List<string> { "tech", "dev" },
            ImageUrls = new List<string> { "https://img.com/1.png" }
        };


        var response = await _client.PostAsJsonAsync("/api/blog", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var created = await response.Content.ReadFromJsonAsync<BlogPostDto>();
        created.Should().NotBeNull();
        created!.Title.Should().Be(dto.Title);
        created.Slug.Should().Be("new-blog-post");
        created.Tags.Should().Contain("tech");
        created.ImageUrls.Should().Contain("https://img.com/1.png");
    }

    [Fact]
    public async Task Update_Post_As_Admin_Returns_NoContent()
    {
        var token = await LoginAndGetTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var dto = new BlogPostDto
        {
            Id = 1,
            Title = "Updated Title",
            Content = "Updated Content",
            Author = "Updated Author",
            PublishedAt = DateTime.UtcNow,
            Tags = new List<string> { "update", "news" },
            ImageUrls = new List<string> { "https://img.com/updated.png" }
        };



        var response = await _client.PutAsJsonAsync("/api/blog/1", dto);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_Post_As_Admin_Returns_NoContent()
    {
        var token = await LoginAndGetTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.DeleteAsync("/api/blog/1");
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
