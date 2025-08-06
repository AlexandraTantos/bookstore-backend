using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.GetAuthorById;

public class GetAuthorByIdHandler(IAuthorRepository authorRepository, IMapper mapper)
    : IRequestHandler<GetAuthorByIdRequest, GetAuthorByIdResponse>
{
    public async Task<GetAuthorByIdResponse> Handle(GetAuthorByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var author = await authorRepository.GetByIdAsync(request.Id, cancellationToken);
            var authorDto = mapper.Map<AuthorDto>(author);
            return new GetAuthorByIdResponse
            {
                Author = authorDto,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch(Exception ex)
        {
            return new GetAuthorByIdResponse
            {
                Message = "Server error,try again later",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
            };
        }
    }
}