namespace Magato.Api.DTO;
public class ProductInquiryDto
{
    public int ProductId
    {
        get; set;
    } 
    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
}
