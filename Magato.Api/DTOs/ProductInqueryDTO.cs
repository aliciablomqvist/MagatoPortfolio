namespace Magato.Api.DTO;
public class ProductInquiryDto
{
    public int ProductId
    {
        get; set;
    }
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public string? Size
    {
        get; set;
    }

    //Honeypot
    public string? Honeypot
    {
        get; set;
    }
}
