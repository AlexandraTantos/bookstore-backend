namespace BookStore.Abstraction
{
  public interface IRepository<T>
  {
    Task<string> InsertAsync(T item, CancellationToken cancellationToken);

    Task DeleteAsync(string id, CancellationToken cancellationToken);

    Task<T> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

    Task<bool> UpdateAsync(T item, CancellationToken cancellationToken);
  }
}
