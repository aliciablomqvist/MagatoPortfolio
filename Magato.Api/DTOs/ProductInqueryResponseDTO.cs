using Magato.Api.Models;
namespace Magato.Api.DTO;

public class ProductInquiryResponseDto
{
    public int Id
    {
        get; set;
    } //Fundera på om detta ska vara här eller flyttas
    public string ProductTitle { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public string? Size
    {
        get; set;
    }
    public bool IsHandled
    {
        get; set;
    }
    public DateTime SentAt
    {
        get; set;
    }
}
