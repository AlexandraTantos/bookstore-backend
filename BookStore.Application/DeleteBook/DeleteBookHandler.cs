using System.Net;
using AutoMapper;
using BookStore.Repositories;
using MediatR;

namespace BookStore.Application.DeleteBook;

public class DeleteBookHandler: IRequestHandler<DeleteBookRequest, DeleteBookResponse>
{
    private IBookRepository bookRepository;

    public DeleteBookHandler(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }

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
                Message = "Server error, try again later"
            };
        }
    }
}