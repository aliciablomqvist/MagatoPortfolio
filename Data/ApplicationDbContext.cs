using Microsoft.EntityFrameworkCore;
using MagatoBackend.Models;

namespace MagatoBackend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Collection> Collections { get; set; }

}
