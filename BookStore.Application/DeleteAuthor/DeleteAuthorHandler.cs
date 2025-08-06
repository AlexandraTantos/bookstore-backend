using System.Net;
using BookStore.Abstraction;
using MediatR;

namespace BookStore.Application.DeleteAuthor;

public class DeleteAuthorHandler(IAuthorRepository authorRepository)
    : IRequestHandler<DeleteAuthorRequest, DeleteAuthorResponse>
{
    public async Task<DeleteAuthorResponse> Handle(DeleteAuthorRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await authorRepository.DeleteAsync(request.Id,cancellationToken);
            return new DeleteAuthorResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = $"Author successfully deleted"
            };
        }
        catch (Exception ex)
        {
            return new DeleteAuthorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}