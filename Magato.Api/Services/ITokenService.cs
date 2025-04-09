using Magato.Api.DTO;
using Magato.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Magato.Api.Services;
public interface ITokenService
{
    string GenerateToken(User user);
}
