using BookStore.Domain;

namespace BookStore.Abstraction
{
  public interface IPublisherRepository : IRepository<Publisher>
  {
    Task<List<Publisher>> GetAllAsync(
      int? skip = null,
      int? take = null,
      string? sortBy = null,
      string? sortOrder = null,
      string? nameFilter = null,
      CancellationToken cancellationToken = default);
  }
}