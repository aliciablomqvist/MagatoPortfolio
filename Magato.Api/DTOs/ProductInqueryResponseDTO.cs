public class ProductInquiryResponseDto
{
    public string ProductTitle { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime SentAt { get; set; }
}
