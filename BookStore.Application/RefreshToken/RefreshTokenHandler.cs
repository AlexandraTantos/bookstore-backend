using System.Net;
using BookStore.Abstraction;
using MediatR;

namespace BookStore.Application.RefreshToken;
public class RefreshTokenHandler(IAuth authService) : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
{
    public Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var newTokens = authService.GetNewPairs(request.RefreshToken);

        if (newTokens == null)
        {
            return Task.FromResult(new RefreshTokenResponse
            {
                Message = "Invalid or expired refresh token",
                StatusCode = HttpStatusCode.Unauthorized
            });
        }

        return Task.FromResult(new RefreshTokenResponse(newTokens.Value.accessToken, newTokens.Value.refreshToken));
    }
}