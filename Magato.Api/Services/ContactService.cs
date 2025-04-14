using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Shared;
using Magato.Api.Validators;
namespace Magato.Api.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _repo;
    private readonly ContactMessageValidator _validator;

    public ContactService(IContactRepository repo)
    {
        _repo = repo;
        _validator = new ContactMessageValidator();
    }

    public async Task<Result> HandleContactAsync(ContactMessageDto dto)
    {
        var errors = _validator.Validate(dto);
        if (errors.Any())
            return Result.Failure(errors);

        var message = new ContactMessage
        {
            Name = dto.Name,
            Email = dto.Email,
            Message = dto.Message,
            GdprConsent = dto.GdprConsent
        };

        await _repo.AddAsync(message);

        return Result.Success();
    }

    public async Task<IEnumerable<ContactMessage>> GetAllMessagesAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<bool> DeleteMessageAsync(int id)
    {
        return await _repo.DeleteAsync(id);
    }

}
