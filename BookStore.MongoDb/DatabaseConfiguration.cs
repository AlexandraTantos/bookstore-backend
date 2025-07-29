
namespace BookStore.MongDb
{
  using BookStore.Abstraction;

  public class DatabaseConfiguration : IDatabaseConfiguration
  {
    public string? ConnectionString { get; set; }

    public string? DatabaseName { get; set; }
  }
}
