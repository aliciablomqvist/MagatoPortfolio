using Magato.Api.Data;
using Magato.Api.Models;

using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll() =>
        _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .ToList();

    public Product? Get(int id) =>
        _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .FirstOrDefault(p => p.Id == id);

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
