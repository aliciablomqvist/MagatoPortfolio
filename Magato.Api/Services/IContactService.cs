// <copyright file="IContactService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;
using Magato.Api.Shared;

namespace Magato.Api.Services;
public interface IContactService
{
    Task<Result> HandleContactAsync(ContactMessageDto dto);

    Task<IEnumerable<ContactMessage>> GetAllMessagesAsync();

    Task<bool> DeleteMessageAsync(int id);
}
