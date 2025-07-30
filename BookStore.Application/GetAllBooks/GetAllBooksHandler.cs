using AutoMapper;
using BookStore.Domain;
using BookStore.Repositories;
using MediatR;

namespace BookStore.Application.GetAllBooks;

public class GetAllBooksHandler :IRequestHandler<GetAllBooksRequest, GetAllBooksResponse>
{
    private IBookRepository bookRepository;
    private IMapper mapper;

    public GetAllBooksHandler(IBookRepository bookRepository, IMapper mapper)
    {
        this.bookRepository = bookRepository;
        this.mapper = mapper;
    }

    public async Task<GetAllBooksResponse> Handle(GetAllBooksRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var books = await this.bookRepository.GetAllAsync(cancellationToken);
            var bookDtos = mapper.Map<List<BookDto>>(books);
            return new GetAllBooksResponse
            {
                Books = bookDtos,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
            return new GetAllBooksResponse
            {
                Message = "Server error,try again later",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
            };
        }
    }
}