using MediatR;

namespace BookStore.Application.GetAllBooks;

public class GetAllBooksRequest :IRequest<GetAllBooksResponse>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public string? Title { get; set; }
    public int? Year { get; set; }
}