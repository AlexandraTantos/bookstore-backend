using System.Net;
using BookStore.Repositories;
using MediatR;

namespace BookStore.Application.DeleteBook;

public class DeleteBookHandler(IBookRepository bookRepository) : IRequestHandler<DeleteBookRequest, DeleteBookResponse>
{
    public async Task<DeleteBookResponse> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await bookRepository.DeleteAsync(request.Id, cancellationToken);
            return new DeleteBookResponse
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Book deleted successfully"
            };
        }
        catch (Exception ex)
        {
            return new DeleteBookResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Server error, try again later: " + ex.Message
            };
        }
    }
}