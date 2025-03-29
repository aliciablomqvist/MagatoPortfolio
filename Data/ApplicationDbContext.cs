using Microsoft.EntityFrameworkCore;
using MagatoBackend.Models;

namespace MagatoBackend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Collection> Collections { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<ColorOption> Colors { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Sketch> Sketches { get; set; }

}
