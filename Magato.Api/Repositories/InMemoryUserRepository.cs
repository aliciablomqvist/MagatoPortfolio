using Magato.Api.Models;

namespace Magato.Api.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public void Add(User user)
    {
        user.Id = _users.Count + 1;
        _users.Add(user);
    }

    public User? GetByUsername(string username)
        => _users.FirstOrDefault(u => u.Username == username);

    public User? GetAdmin()
        => _users.FirstOrDefault(u => u.IsAdmin);

    public bool AdminExists()
        => _users.Any(u => u.IsAdmin);

    public IEnumerable<User> GetAll() => _users;
}
