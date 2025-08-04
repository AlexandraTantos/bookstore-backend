using MediatR;

namespace BookStore.Application.GetAllAuthors;

public class GetAllAuthorsRequest : IRequest<GetAllAuthorsResponse>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? BirthDate { get; set; }
}