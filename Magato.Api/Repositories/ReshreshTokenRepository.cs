using Magato.Api.Data;


public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    public RefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);
        _context.SaveChanges();
    }

    public RefreshToken? Get(string token)
    {
        return _context.RefreshTokens.FirstOrDefault(t => t.Token == token);
    }

    public void Revoke(string token)
    {
        var refresh = Get(token);
        if (refresh != null)
        {
            refresh.IsRevoked = true;
            _context.SaveChanges();
        }
    }
}
