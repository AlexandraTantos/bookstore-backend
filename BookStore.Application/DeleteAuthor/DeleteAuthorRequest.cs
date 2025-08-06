using MediatR;

namespace BookStore.Application.DeleteAuthor;

public class DeleteAuthorRequest : IRequest<DeleteAuthorResponse>
{
    public string Id { get; init; } = null!;
}