using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Models;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
}
