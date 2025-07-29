using BookStore.Domain;
using MediatR;

namespace BookStore.Application.LoginUser;

public class LoginUserRequest : IRequest<LoginUserResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}