namespace Magato.Api.DTO
{
    public class BlogPostDto
    {
        public int Id
        {
            get; set;
        }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Author
        {
            get; set;
        }
        public DateTime PublishedAt
        {
            get; set;
        }
    }
}
