// <copyright file="ContactService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Shared;
using Magato.Api.Validators;

public class ContactService : IContactService
{
    private readonly IContactRepository repo;
    private readonly ContactMessageValidator validator;

    public ContactService(IContactRepository repo)
{
        this.repo = repo;
        this.validator = new ContactMessageValidator();
    }

    public async Task<Result> HandleContactAsync(ContactMessageCreateDto dto)
{
        var errors = this.validator.ValidateAndExtractErrors(dto);
        if (errors.Any())
{
            return Result.Failure(errors);
        }

        var message = new ContactMessage
{
            Name = dto.Name,
            Email = dto.Email,
            Message = dto.Message,
            GdprConsent = dto.GdprConsent,
        };

        await this.repo.AddAsync(message);

        return Result.Success();
    }

    public async Task<IEnumerable<ContactMessage>> GetAllMessagesAsync()
{
        return await this.repo.GetAllAsync();
    }

    public async Task<bool> DeleteMessageAsync(int id)
{
        return await this.repo.DeleteAsync(id);
    }
}
