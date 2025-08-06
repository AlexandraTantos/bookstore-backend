using System.Net;

namespace BookStore.Application.RefreshToken;

public class RefreshTokenResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public string Message { get; init; } = null!;
    public HttpStatusCode StatusCode { get; init; }

    public RefreshTokenResponse() { }

    public RefreshTokenResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        Message = "Tokens refreshed successfully";
        StatusCode = HttpStatusCode.OK;
    }
}