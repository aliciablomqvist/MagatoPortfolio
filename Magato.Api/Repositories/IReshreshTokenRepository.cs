public interface IRefreshTokenRepository
{
    void Add(RefreshToken token);
    RefreshToken? Get(string token);
    void Revoke(string token);
}
