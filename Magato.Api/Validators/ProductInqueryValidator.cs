using FluentValidation;

using Magato.Api.DTO;

namespace Magato.Api.Validators;
public class ProductInquiryValidator : AbstractValidator<ProductInquiryDto>
{
    private const string EmailRegex =
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    public ProductInquiryValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .WithMessage("Invalid product.");


        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(EmailRegex).WithMessage("Invalid email format.")
            .MaximumLength(200);
        RuleFor(x => x.Message)
            .NotEmpty()
            .MaximumLength(1000)
            .WithMessage("Message is too long.");

        RuleFor(x => x.Size)
            .MaximumLength(20)
            .WithMessage("Size input is too long.");

        //Honeypot för spam
        RuleFor(x => x.Honeypot).Must(string.IsNullOrWhiteSpace)
.WithMessage("Potential spam detected.");

    }
}
