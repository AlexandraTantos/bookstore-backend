using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.GetAllAuthors;

public class GetAllAuthorsHandler :IRequestHandler<GetAllAuthorsRequest, GetAllAuthorsResponse>
{
    private IAuthorRepository authorRepository;
    private IMapper mapper;

    public GetAllAuthorsHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        this.authorRepository = authorRepository;
        this.mapper = mapper;
    }
    public async Task<GetAllAuthorsResponse> Handle(GetAllAuthorsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var authors = await this.authorRepository.GetAllAsync(cancellationToken);
            var authorDtos = mapper.Map<List<AuthorDto>>(authors);
            return new GetAllAuthorsResponse
            {
                Authors = authorDtos,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new GetAllAuthorsResponse
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}