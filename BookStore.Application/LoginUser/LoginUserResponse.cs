using System.Net;

namespace BookStore.Application.LoginUser;

public class LoginUserResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public string Message { get; set; } = null!;
    public HttpStatusCode StatusCode { get; init; }

    public LoginUserResponse() { }

    public LoginUserResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        Message = "Login successful";
        StatusCode = HttpStatusCode.OK;
    }
}
