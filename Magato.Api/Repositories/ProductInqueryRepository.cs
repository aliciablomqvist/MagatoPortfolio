using Magato.Api.Data;
using Magato.Api.Models;

using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories;

public class ProductInquiryRepository : IProductInquiryRepository
{
    private readonly ApplicationDbContext _context;

    public ProductInquiryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ProductInquiry> GetAll()
    {
        return _context.ProductInquiries
            .Include(i => i.Product)
            .OrderByDescending(i => i.SentAt)
            .ToList();
    }

    public ProductInquiry? Get(int id)
    {
        return _context.ProductInquiries
            .Include(i => i.Product)
            .FirstOrDefault(i => i.Id == id);
    }

    public void Add(ProductInquiry inquiry)
    {
        _context.ProductInquiries.Add(inquiry);
        _context.SaveChanges();
    }

    public void Update(ProductInquiry inquiry)
    {
        _context.ProductInquiries.Update(inquiry);
        _context.SaveChanges();
    }
}
