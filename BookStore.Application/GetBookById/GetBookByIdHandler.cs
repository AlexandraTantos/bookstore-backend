using System.Net;
using AutoMapper;
using BookStore.Domain;
using BookStore.Repositories;
using MediatR;

namespace BookStore.Application.GetBookById;

public class GetBookByIdHandler : IRequestHandler<GetBookByIdRequest, GetBookByIdResponse>
{
    private readonly IBookRepository bookRepository;
    private readonly IMapper mapper;

    public GetBookByIdHandler(IBookRepository bookRepository, IMapper mapper)
    {
        this.bookRepository = bookRepository;
        this.mapper = mapper;
    }

    public async Task<GetBookByIdResponse> Handle(GetBookByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var book = await this.bookRepository.GetByIdAsync(request.Id, cancellationToken);
            var bookDto = mapper.Map<BookDto>(book);
            return new GetBookByIdResponse
            {
                Book = bookDto,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new GetBookByIdResponse
            {
                Message = "Server error,try again later",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
            };
        }
    }
}