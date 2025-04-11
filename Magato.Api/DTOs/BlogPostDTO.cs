using System.ComponentModel.DataAnnotations;

namespace Magato.Api.DTO
{
    public class BlogPostDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? Summary
        {
            get; set;
        }

        public bool Published
        {
            get; set;
        }

        public DateTime? CreatedAt
        {
            get; set;
        }
        public DateTime? UpdatedAt
        {
            get; set;
        }
        public List<string> ImageUrls { get; set; } = new();
    }
}
