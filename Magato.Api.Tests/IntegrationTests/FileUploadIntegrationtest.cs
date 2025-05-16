

namespace Magato.Api.Tests.IntegrationTests
{
    public class UploadControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
        private readonly HttpClient _client;

        public UploadControllerTests(WebApplicationFactory<Program> factory)
{
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
{
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task UploadImage_ReturnsOk_AndImageUrl()
{

            var content = new MultipartFormDataContent();
            var fileBytes = new byte[]{ 1, 2, 3, 4, 5 };
            var byteArrayContent = new ByteArrayContent(fileBytes);
            byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
            content.Add(byteArrayContent, "file", "test.png");

            var response = await _client.PostAsync("/api/upload", content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseData = await response.Content.ReadFromJsonAsync<UploadResponse>();
            responseData.Should().NotBeNull();
            responseData!.ImageUrl.Should().Contain("/uploads/test.png");
        }

        private class UploadResponse
{
            public string ImageUrl{ get; set; } = string.Empty;
        }
    }
}
