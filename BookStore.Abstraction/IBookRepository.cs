namespace BookStore.Repositories
{
  using BookStore.Abstraction;
  using BookStore.Domain;

  public interface IBookRepository : IRepository<Book>
  {
    
  }
}
