using BookStore.Domain;
using MediatR;

namespace BookStore.Application.CreateAuthor;

public class CreateAuthorRequest : IRequest<CreateAuthorResponse>
{
    public AuthorDto AuthorDto { get; set; }
}