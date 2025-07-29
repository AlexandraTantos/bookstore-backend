using BookStore.Domain;
using MediatR;

namespace BookStore.Application.InsertUser;

public class InsertUserRequest : IRequest<InsertUserResponse>
{
    public UserDto UserDto { get; set; }
}