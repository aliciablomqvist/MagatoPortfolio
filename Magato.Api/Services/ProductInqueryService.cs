using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Shared;

namespace Magato.Api.Services;
public class ProductInquiryService : IProductInquiryService
{
    private readonly IProductInquiryRepository _repo;
    private readonly IProductRepository _productRepo;

    public ProductInquiryService(IProductInquiryRepository repo, IProductRepository productRepo)
    {
        _repo = repo;
        _productRepo = productRepo;
    }

    public ProductInquiryResponseDto Add(ProductInquiryDto dto)
    {
        var inquiry = new ProductInquiry
        {
            ProductId = dto.ProductId,
            Email = dto.Email,
            Message = dto.Message,
            Size = dto.Size,

        };

        _repo.Add(inquiry);

        var product = _productRepo.Get(dto.ProductId);

        return new ProductInquiryResponseDto //Anpassa för kund. Vad hen ska se vid bekräftelse.
        {
            Id = inquiry.Id,
            ProductTitle = product?.Title ?? "Unknown",
            Email = dto.Email,
            Message = dto.Message,
            Size = dto.Size,
            SentAt = inquiry.SentAt,
            IsHandled = false
        };
    }

    public IEnumerable<ProductInquiryResponseDto> GetAll()
    {
        return _repo.GetAll()
            .Select(i => new ProductInquiryResponseDto
            {
                Id = i.Id,
                ProductTitle = i.Product?.Title ?? "Unknown",
                Email = i.Email,
                Message = i.Message,
                Size = i.Size,
                SentAt = i.SentAt,
                IsHandled = i.IsHandled
            });
    }

    public ProductInquiryResponseDto? GetById(int id)
    {
        var inquiry = _repo.Get(id);
        if (inquiry == null)
            return null;

        return new ProductInquiryResponseDto
        {
            Id = inquiry.Id,
            ProductTitle = inquiry.Product?.Title ?? "Unknown",
            Email = inquiry.Email,
            Message = inquiry.Message,
            Size = inquiry.Size,
            SentAt = inquiry.SentAt,
            IsHandled = inquiry.IsHandled
        };
    }

    public void MarkAsHandled(int id)
    {
        var inquiry = _repo.Get(id);
        if (inquiry != null)
        {
            inquiry.IsHandled = true;
            _repo.Update(inquiry);
        }
    }
}
