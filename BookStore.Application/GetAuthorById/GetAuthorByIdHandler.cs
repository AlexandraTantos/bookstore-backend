using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.GetAuthorById;

public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdRequest, GetAuthorByIdResponse>
{
    private readonly IAuthorRepository authorRepository;
    private readonly IMapper mapper;

    public GetAuthorByIdHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        this.authorRepository = authorRepository;
        this.mapper = mapper;
    }
    public async Task<GetAuthorByIdResponse> Handle(GetAuthorByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var author = await this.authorRepository.GetByIdAsync(request.Id, cancellationToken);
            var authorDto = mapper.Map<AuthorDto>(author);
            return new GetAuthorByIdResponse
            {
                Author = authorDto,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new GetAuthorByIdResponse
            {
                Message = "Server error,try again later",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
            };
        }
    }
}