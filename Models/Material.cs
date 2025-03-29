using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MagatoBackend.Models;
public class Material
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CollectionId { get; set; }

    [JsonIgnore]
    public Collection? Collection { get; set; }
}