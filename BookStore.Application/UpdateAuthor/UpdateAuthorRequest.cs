using MediatR;

namespace BookStore.Application.UpdateAuthor;

public class UpdateAuthorRequest: IRequest<UpdateAuthorResponse>
{
    public string? Id  { get; set; }
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public DateTime? BirthDate { get; set; }
    public string? Nationality {get; set;} = null!;
    public List<string>? SpokenLanguages { get; set; }
}