using Microsoft.EntityFrameworkCore;

namespace MagatoBackend.Models; 

public class Collection
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
}
