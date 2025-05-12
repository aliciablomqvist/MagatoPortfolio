public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _repo;

    public RefreshTokenService(IRefreshTokenRepository repo)
    {
        _repo = repo;
    }

    public RefreshToken CreateAndStore(string username)
    {
        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        var refresh = new RefreshToken
        {
            Token = token,
            Username = username,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        _repo.Add(refresh);
        return refresh;
    }

    public RefreshToken? Get(string token) => _repo.Get(token);

    public void Revoke(string token) => _repo.Revoke(token);
}
