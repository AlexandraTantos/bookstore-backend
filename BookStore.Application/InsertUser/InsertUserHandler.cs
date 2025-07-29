using System.Net;
using AutoMapper;
using BookStore.Application.CreateBook;
using BookStore.Domain;
using BookStore.Repositories;
using MediatR;

namespace BookStore.Application.InsertUser;

public class InsertUserHandler : IRequestHandler<InsertUserRequest, InsertUserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public InsertUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;

    }

    public async Task<InsertUserResponse> Handle(InsertUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            UserDto userDto = request.UserDto;
            string passwordHash = PasswordHashing.HashPassword(userDto.Password);
            User user = this.mapper.Map<User>(userDto);
            user.Role = Role.User; 
            user.PasswordHash = passwordHash;
            var response = await this.userRepository.InsertAsync(user, cancellationToken);
            return new InsertUserResponse(response);
        }
        catch (Exception ex)
        {
            return new InsertUserResponse
            {
                Message = "Server error,try again later",
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }
}
