public interface IRefreshTokenService
{
    RefreshToken CreateAndStore(string username);
    RefreshToken? Get(string token);
    void Revoke(string token);
}
