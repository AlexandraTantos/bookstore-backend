using MongoDB.Driver;

namespace BookStore.Abstraction
{
  public interface IDatabase
  {
    IMongoCollection<TClass> GetMongoCollection<TClass>(string name)
      where TClass : class;
  }
}
