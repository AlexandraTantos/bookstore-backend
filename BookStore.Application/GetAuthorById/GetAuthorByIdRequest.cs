using MediatR;

namespace BookStore.Application.GetAuthorById;

public class GetAuthorByIdRequest : IRequest<GetAuthorByIdResponse>
{
    public string Id { get; init; } = null!;
}