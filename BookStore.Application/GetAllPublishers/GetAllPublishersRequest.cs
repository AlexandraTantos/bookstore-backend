using MediatR;

namespace BookStore.Application.GetAllPublishers;

public class GetAllPublishersRequest : IRequest<GetAllPublishersResponse>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public string? Name { get; set; }
}