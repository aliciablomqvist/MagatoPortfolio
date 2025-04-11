using Microsoft.EntityFrameworkCore;

namespace Magato.Api.Models;
public class PageContent
{
    public int Id { get; set; }
    public string Key { get; set; } = null!; // Såsom "AboutMe", "StartPage" osv
    public string Value { get; set; } = null!; //Innehållet i texten
}
