using MediatR;

namespace BookStore.Application.DeleteBook;

public class DeleteBookRequest :IRequest<DeleteBookResponse>
{
    public string Id {get; init;} = null!;
}