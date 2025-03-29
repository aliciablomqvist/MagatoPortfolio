using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MagatoBackend.Models;

public class ColorOption
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Hex { get; set; } 
    public int CollectionId { get; set; }

//Koppling till collection
    [JsonIgnore]
    public Collection? Collection { get; set; }
}