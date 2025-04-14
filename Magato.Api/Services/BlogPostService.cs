using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;

namespace Magato.Api.Services;
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _repo;

        public BlogPostService(IBlogPostRepository repo)
        {
            _repo = repo;
        }

        public BlogPostDto? Get(int id)
        {
            var post = _repo.Get(id);
            return post == null ? null : Map(post);
        }

        public IEnumerable<BlogPostDto> GetAll()
            => _repo.GetAll().Select(Map);

    public void Add(BlogPostDto dto)
    {
        dto.Slug = GenerateSlug(dto.Title);
        _repo.Add(Map(dto));
    }
    public void Update(BlogPostDto dto)
    {
        dto.Slug = GenerateSlug(dto.Title);
        _repo.Update(Map(dto));
    }

    public void Delete(int id)
            => _repo.Delete(id);

    private static BlogPostDto Map(BlogPost post) => new()
    {
        Id = post.Id,
        Title = post.Title,
        Slug = post.Slug,
        Content = post.Content,
        Author = post.Author,
        PublishedAt = post.PublishedAt,
        Tags = post.Tags,
        ImageUrls = post.ImageUrls
    };

    private static BlogPost Map(BlogPostDto dto) => new()
    {
        Id = dto.Id,
        Title = dto.Title,
        Slug = dto.Slug,
        Content = dto.Content,
        Author = dto.Author,
        PublishedAt = dto.PublishedAt,
        Tags = dto.Tags,
        ImageUrls = dto.ImageUrls
    };

    private static string GenerateSlug(string title)
    {
        return title.ToLower()
            .Replace(" ", "-")
            .Replace("å", "a")
            .Replace("ä", "a")
            .Replace("ö", "o")
            .Replace(".", "")
            .Replace(",", "")
            .Replace("!", "")
            .Replace("?", "")
            .Replace(":", "")
            .Replace(";", "")
            .Replace("/", "-")
            .Replace("\\", "-");
    }

    public BlogPostDto? GetBySlug(string slug)
    {
        var post = _repo.GetBySlug(slug);
        return post == null ? null : Map(post);
    }

}
