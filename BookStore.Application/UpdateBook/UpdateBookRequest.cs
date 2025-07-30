using MediatR;

namespace BookStore.Application.UpdateBook;

public class UpdateBookRequest :IRequest<UpdateBookResponse>
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? AuthorId { get; set; }
    public DateTime? YearOfPublication { get; set; }
    public List<string>? Genres { get; set; }
    public string? PublisherId { get; set; }
}