using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using MediatR;

namespace BookStore.Application.DeleteAuthor;

public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorRequest, DeleteAuthorResponse>
{
    private readonly IAuthorRepository authorRepository;
    private IMapper  mapper;

    public DeleteAuthorHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        this.authorRepository = authorRepository;
        this.mapper = mapper;
    }
    
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