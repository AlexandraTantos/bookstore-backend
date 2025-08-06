using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.InsertUser;

public class InsertUserHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<InsertUserRequest, InsertUserResponse>
{
    public async Task<InsertUserResponse> Handle(InsertUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            UserDto userDto = request.UserDto;
            string passwordHash = PasswordHashing.HashPassword(userDto.Password);
            User user = mapper.Map<User>(userDto);
            user.Role = Role.User; 
            user.PasswordHash = passwordHash;
            var response = await userRepository.InsertAsync(user, cancellationToken);
            return new InsertUserResponse(response);
        }
        catch (Exception ex)
        {
            return new InsertUserResponse
            {
                Message = "Server error,try again later" + ex.Message,
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }
}
