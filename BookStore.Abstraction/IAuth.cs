namespace BookStore.Abstraction
{
  public interface IAuth
  {
    string GenerateAccessToken(string userId, string role);

    string GenerateRefreshToken(string userId, string role);

    void BlackListToken(string accessToken);

    void BlackListRefreshToken(string refreshToken);

    (string accessToken, string refreshToken)? GetNewPairs(string refreshToken);

    bool ValidateRole(string accessToken, string userRole);

  }
}
