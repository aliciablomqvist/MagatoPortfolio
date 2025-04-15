using Magato.Api.Models;

namespace Magato.Api.Repositories;

public interface IProductInquiryRepository
{
    void Add(ProductInquiry inquiry);
    IEnumerable<ProductInquiry> GetAll();
    ProductInquiry? Get(int id);

}
