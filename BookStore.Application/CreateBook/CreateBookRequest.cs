using BookStore.Domain;
using MediatR;

namespace BookStore.Application.CreateBook
{
  public class CreateBookRequest : IRequest<CreateBookResponse>
  {
    public BookDto BookDto {  get; set; }
  }
}
