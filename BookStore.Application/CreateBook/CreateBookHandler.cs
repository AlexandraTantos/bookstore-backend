using AutoMapper;
using BookStore.Domain;
using BookStore.Repositories;
using MediatR;

namespace BookStore.Application.CreateBook
{
  public class CreateBookHandler : IRequestHandler<CreateBookRequest, CreateBookResponse>
  {
    private readonly IBookRepository bookRepository;
    private readonly IMapper mapper;
    public CreateBookHandler(IBookRepository bookRepository, IMapper mapper)
    {
      this.bookRepository = bookRepository;
      this.mapper = mapper;
    }
    public async Task<CreateBookResponse> Handle(CreateBookRequest request, CancellationToken cancellationToken)
    {
      try
      {
        BookDto bookDto = request.BookDto;
        Book book = this.mapper.Map<Book>(bookDto);
        var response = await this.bookRepository.InsertAsync(book, cancellationToken);
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
