using Magato.Api.DTO;

namespace Magato.Api.Services;
    public interface IBlogPostService
    {
        BlogPostDto? Get(int id);
        IEnumerable<BlogPostDto> GetAll();
        void Add(BlogPostDto dto);
        void Update(BlogPostDto dto);
        void Delete(int id);
    }
