using System.Net;

namespace BookStore.Application.LoginUser;

public class LoginUserResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public LoginUserResponse() { }

    public LoginUserResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        Message = "Login successful";
        StatusCode = HttpStatusCode.OK;
    }
}
