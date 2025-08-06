using AutoMapper;
using BookStore.Domain;
using BookStore.Repositories;
using MediatR;

namespace BookStore.Application.CreateBook
{
  public class CreateBookHandler(IBookRepository bookRepository, IMapper mapper)
    : IRequestHandler<CreateBookRequest, CreateBookResponse>
  {
    public async Task<CreateBookResponse> Handle(CreateBookRequest request, CancellationToken cancellationToken)
    {
      try
      {
        BookDto bookDto = request.BookDto;
        Book book = mapper.Map<Book>(bookDto);
        var response = await bookRepository.InsertAsync(book, cancellationToken);
        return new CreateBookResponse(response);
      }
      catch (Exception ex)
      {
        return new CreateBookResponse
        {
          Message = "Server error,try again later",
          StatusCode = System.Net.HttpStatusCode.InternalServerError
        };
      }
    }
  }
}
