using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Repositories;

namespace Magato.Api.Services;
public interface IEmailService
{
    Task SendContactNotificationAsync(ContactMessageDto dto);
}
