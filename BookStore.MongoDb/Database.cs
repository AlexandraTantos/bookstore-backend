namespace BookStore.MongDb
{
  using BookStore.Abstraction;
  using MongoDB.Driver;

  public class Database : IDatabase
  {
    private readonly IMongoDatabase db;
    private readonly MongoClient client;

    public Database(IDatabaseConfiguration configuration)
    {
      this.client = new MongoClient(configuration.ConnectionString);
      this.db = client.GetDatabase(configuration.DatabaseName);
    }

    IMongoCollection<TClass> IDatabase.GetMongoCollection<TClass>(string name)
    {
      return this.db.GetCollection<TClass>(name);
    }

  }
}
