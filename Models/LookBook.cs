using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Models;

public class LookbookImage
{
    public int Id { get; set; }
    public string Url { get; set; } = "";
     public string Description { get; set; } = "";
     
    public int CollectionId { get; set; }

    [JsonIgnore]
    public Collection? Collection { get; set; }
}
