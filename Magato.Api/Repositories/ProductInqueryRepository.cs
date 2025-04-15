using Magato.Api.Models;
using Magato.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Repositories;

public class ProductInquiryRepository : IProductInquiryRepository
{
    private readonly ApplicationDbContext _context;
    public ProductInquiryRepository(ApplicationDbContext context) => _context = context;

    public void Add(ProductInquiry inquiry)
    {
        _context.ProductInquiries.Add(inquiry);
        _context.SaveChanges();
    }

    public IEnumerable<ProductInquiry> GetAll()
    {
        return _context.ProductInquiries
                       .Include(i => i.Product)
                       .ToList();
    }

    public ProductInquiry? Get(int id)
    {
        return _context.ProductInquiries
            .Include(i => i.Product)
            .FirstOrDefault(i => i.Id == id);
    }
}



