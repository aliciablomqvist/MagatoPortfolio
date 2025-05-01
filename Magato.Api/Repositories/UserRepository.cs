using Magato.Api.Models;
using Magato.Api.Data;

namespace Magato.Api.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User? GetByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }

    public User? GetAdmin()
    {
        return _context.Users.FirstOrDefault(u => u.IsAdmin);
    }

    public bool AdminExists()
    {
        return _context.Users.Any(u => u.IsAdmin);
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }
}
