using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.CreateAuthor;

public class CreateAuthorHandler(IAuthorRepository authorRepository, IMapper mapper)
    : IRequestHandler<CreateAuthorRequest, CreateAuthorResponse>
{
    public async Task<CreateAuthorResponse> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
    {
        try
        {
            AuthorDto authorDto = request.AuthorDto;
            Author author = mapper.Map<Author>(authorDto);
            var response = await authorRepository.InsertAsync(author,cancellationToken);
            return new CreateAuthorResponse(response);
        }
        catch (Exception ex)
        {
            return new CreateAuthorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}