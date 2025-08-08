using System.Net;
using BookStore.Domain;

namespace BookStore.Application.GetAllUsers;

public class GetAllUsersResponse
{
    public List<UserDto>  Users { get; set; } = new();
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}