namespace BookStore.Repositories
{
  using BookStore.Abstraction;
  using BookStore.Domain;

  public interface IBookRepository : IRepository<Book>
  {
    Task<List<Book>> GetAllAsync(
      int? skip = null,
      int? take = null,
      string? sortBy = null,
      string? sortOrder = null,
      string? titleFilter = null,
      int? yearFilter = null,
      CancellationToken cancellationToken = default);
  }
}
