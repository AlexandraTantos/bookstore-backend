using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.GetAllUsers;

public class GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetAllUsersRequest, GetAllUsersResponse>
{
    public async Task<GetAllUsersResponse> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await userRepository.GetAllAsync(cancellationToken);
            var usersDto = mapper.Map<List<UserDto>>(users);
            return new GetAllUsersResponse
            {
                Users = usersDto,
                StatusCode = HttpStatusCode.OK,
                Message = "Success"
            };
        }
        catch(Exception ex)
        {
            return new GetAllUsersResponse
            {
                Message = ex.Message,
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }
}