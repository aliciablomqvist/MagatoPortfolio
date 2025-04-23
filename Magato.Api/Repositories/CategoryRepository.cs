using Magato.Api.Data;
using Magato.Api.Models;

namespace Magato.Api.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Category> GetAll() => _context.Categories.ToList();

    public void Add(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }
}
