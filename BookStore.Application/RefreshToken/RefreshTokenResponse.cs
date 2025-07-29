using System.Net;

namespace BookStore.Application.RefreshToken;

public class RefreshTokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public RefreshTokenResponse() { }

    public RefreshTokenResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        Message = "Tokens refreshed successfully";
        StatusCode = HttpStatusCode.OK;
    }
}