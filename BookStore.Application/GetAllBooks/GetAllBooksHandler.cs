using AutoMapper;
using BookStore.Domain;
using BookStore.Repositories;
using MediatR;

namespace BookStore.Application.GetAllBooks;

public class GetAllBooksHandler(IBookRepository bookRepository, IMapper mapper)
    : IRequestHandler<GetAllBooksRequest, GetAllBooksResponse>
{
    public async Task<GetAllBooksResponse> Handle(GetAllBooksRequest request, CancellationToken cancellationToken)
    {
        int skip = (request.Page - 1) * request.PageSize;
        try
        {
            var books = await bookRepository.GetAllAsync(skip: skip,
                take: request.PageSize,
                sortBy: request.SortBy,
                sortOrder: request.SortOrder,
                titleFilter: request.Title,
                yearFilter: request.Year,
                cancellationToken);
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