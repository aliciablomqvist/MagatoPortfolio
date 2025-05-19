// <copyright file="IContactService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Magato.Api.Services;
using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Shared;

public interface IContactService
{
    Task<Result> HandleContactAsync(ContactMessageCreateDto dto);

    Task<IEnumerable<ContactMessage>> GetAllMessagesAsync();

    Task<bool> DeleteMessageAsync(int id);
}
