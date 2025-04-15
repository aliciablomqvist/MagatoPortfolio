using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Shared;

namespace Magato.Api.Services;
public class ProductInquiryService : IProductInquiryService
{
    private readonly IProductInquiryRepository _repo;
    public ProductInquiryService(IProductInquiryRepository repo) => _repo = repo;

    public int Add(ProductInquiryDto dto)
    {
        var inquiry = new ProductInquiry
        {
            ProductId = dto.ProductId,
            Email = dto.Email,
            Message = dto.Message,
            SentAt = DateTime.UtcNow
        };

        _repo.Add(inquiry);
        return inquiry.Id;
    }

    public IEnumerable<ProductInquiryResponseDto> GetAll()
    {
        return _repo.GetAll()
            .Select(i => new ProductInquiryResponseDto
            {
                ProductTitle = i.Product.Title,
                Email = i.Email,
                Message = i.Message,
                SentAt = i.SentAt
            });
    }
    public ProductInquiryResponseDto? GetById(int id)
    {
        var inquiry = _repo.Get(id);
        if (inquiry == null)
            return null;

        return new ProductInquiryResponseDto
        {
            ProductTitle = inquiry.Product?.Title,
            Email = inquiry.Email,
            Message = inquiry.Message,
            SentAt = inquiry.SentAt
        };
    }
}
