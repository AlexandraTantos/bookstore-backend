namespace BookStore.Domain
{
  public class BookDto
  {
    public string AuthorId { get; set; }

    public string Title { get; set; }

    public DateTime YearOfPublication { get; set; }

    public List<string> Genres { get; set; }
  }
}
