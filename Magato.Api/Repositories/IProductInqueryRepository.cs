using Magato.Api.Models;

namespace Magato.Api.Repositories;

public interface IProductInquiryRepository
{
    IEnumerable<ProductInquiry> GetAll();
    ProductInquiry? Get(int id);
    void Add(ProductInquiry inquiry);
    void Update(ProductInquiry inquiry);
}
