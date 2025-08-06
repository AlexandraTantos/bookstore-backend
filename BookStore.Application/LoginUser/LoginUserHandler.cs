using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.LoginUser;

public class LoginUserHandler(IUserRepository userRepository, IAuth authService, IMapper mapper)
    : IRequestHandler<LoginUserRequest, LoginUserResponse>
{
    private readonly IMapper mapper = mapper;

    public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByEmailAsync(request.Email);
    
        if (user == null)
        {
            return new LoginUserResponse
            {
                Message = "Invalid email or password",
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        bool isPasswordValid = PasswordHashing.VerifyPassword(request.Password, user.PasswordHash);
    
        if (!isPasswordValid)
        {
            return new LoginUserResponse
            {
                Message = "Invalid email or password",
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        string accessToken = authService.GenerateAccessToken(user.Id.ToString(), user.Role.ToString());
        string refreshToken = authService.GenerateRefreshToken(user.Id.ToString(), user.Role.ToString());

        return new LoginUserResponse(accessToken, refreshToken);
    }

}