using Magato.Api.DTO;

namespace Magato.Api.Services;

public interface IProductInquiryService
{
    ProductInquiryResponseDto Add(ProductInquiryDto dto);
    IEnumerable<ProductInquiryResponseDto> GetAll();
    ProductInquiryResponseDto? GetById(int id);
    void MarkAsHandled(int id);
}
