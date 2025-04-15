using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Shared;

namespace Magato.Api.Services;

public interface IProductInquiryService
{
    int Add(ProductInquiryDto dto);
    IEnumerable<ProductInquiryResponseDto> GetAll();
    ProductInquiryResponseDto? GetById(int id);

}
