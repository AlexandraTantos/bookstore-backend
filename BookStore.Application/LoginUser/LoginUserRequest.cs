using BookStore.Domain;
using MediatR;

namespace BookStore.Application.LoginUser;

public class LoginUserRequest : IRequest<LoginUserResponse>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}