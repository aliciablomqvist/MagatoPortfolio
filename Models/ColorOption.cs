using System.Text.Json.Serialization;

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