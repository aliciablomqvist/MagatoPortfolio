using FluentValidation;

using Magato.Api.DTO;

namespace Magato.Api.Validators;
public class ProductInquiryValidator : AbstractValidator<ProductInquiryDto>
{
    public ProductInquiryValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Message).NotEmpty().MaximumLength(1000);
    }
}
