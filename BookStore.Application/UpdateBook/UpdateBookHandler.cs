using System.Net;
using AutoMapper;
using BookStore.Repositories;
using MediatR;

namespace BookStore.Application.UpdateBook;

public class UpdateBookHandler : IRequestHandler<UpdateBookRequest, UpdateBookResponse>
{
    private IBookRepository bookRepository;
    private IMapper mapper;

    public UpdateBookHandler(IBookRepository bookRepository, IMapper mapper)
    {
        this.bookRepository = bookRepository;
        this.mapper = mapper;
    }

    public async Task<UpdateBookResponse> Handle(UpdateBookRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var book = await bookRepository.GetByIdAsync(request.Id, cancellationToken);
            if (book == null)
            {
                return new UpdateBookResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Book not found"
                };
            }

            if (request.Title != null) book.Title = request.Title;
            if (request.YearOfPublication.HasValue) book.YearOfPublication = request.YearOfPublication.Value;
            if (request.Genres != null) book.Genres = request.Genres;
            if (request.AuthorId != null) book.AuthorId = request.AuthorId;
            if (request.PublisherId != null) book.PublisherId = request.PublisherId;

            await bookRepository.UpdateAsync(book, cancellationToken);
            var updatedBook = mapper.Map<BookDto>(book);
            return new UpdateBookResponse
            {
                StatusCode = HttpStatusCode.OK,
                UpdatedBook = updatedBook,
                Message = "Book updated successfully"
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new UpdateBookResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}