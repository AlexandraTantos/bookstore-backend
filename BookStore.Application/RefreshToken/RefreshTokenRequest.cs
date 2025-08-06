using MediatR;

namespace BookStore.Application.RefreshToken;

public class RefreshTokenRequest : IRequest<RefreshTokenResponse>
{
    public string RefreshToken { get; set; } = null!;
}