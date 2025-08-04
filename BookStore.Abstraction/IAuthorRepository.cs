using BookStore.Domain;

namespace BookStore.Abstraction
{
  public interface IAuthorRepository : IRepository<Author>
  {
    Task<List<Author>> GetAllAsync(
      int? skip = null,
      int? take = null,
      string? sortBy = null,
      string? sortOrder = null,
      string? firstNameFilter = null,
      string? lastNameFilter = null,
      int? birthDateFilter = null,
      CancellationToken cancellationToken = default);
  }
}