namespace BookStore.Domain;

public class AuthorDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string Nationality {get; set;} = null!;
    public List<string> SpokenLanguages { get; set; }
}