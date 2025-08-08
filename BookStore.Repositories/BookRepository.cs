using BookStore.Abstraction;
using BookStore.Domain;
using MongoDB.Driver;

namespace BookStore.Repositories
{
  public class BookRepository : IBookRepository
  {
    private IDatabase database;
    private IMongoCollection<Book> booksCollection;
    public BookRepository(IDatabase database)
    {
      this.database = database;
      this.booksCollection = database.GetMongoCollection<Book>("Books");
    }
    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
      var filter = Builders<Book>.Filter.Eq(x => x.Id, id);
      await this.booksCollection.DeleteOneAsync(filter, cancellationToken);
    }
    
    public async Task<Book> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
      var filter = Builders<Book>.Filter.Eq(x => x.Id, id);
      Book book = await this.booksCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
      return book;
    }

    public async Task<List<Book>> GetAllAsync(CancellationToken cancellationToken)
    {
      return await GetAllAsync(null, null, null, null, null, null, cancellationToken);
    }

    public async Task<string> InsertAsync(Book item, CancellationToken cancellationToken)
    {
      await this.booksCollection.InsertOneAsync(item, cancellationToken);
      return item.Id;
    }

    public async Task<bool> UpdateAsync(Book item, CancellationToken cancellationToken)
    {
      var filter = Builders<Book>.Filter.Eq(x => x.Id, item.Id);
      var update = Builders<Book>.Update.Set(x => x.AuthorId, item.AuthorId)
                                        .Set(x => x.Genres, item.Genres)
                                        .Set(x => x.YearOfPublication, item.YearOfPublication)
                                        .Set(x => x.Title, item.Title)
                                        .Set(x => x.PublisherId, item.PublisherId);

      //if we want to specify which properties to update
      var response = await this.booksCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
      
      //if we want to replace the whole document
      //await this.booksCollection.ReplaceOneAsync(filter, item, cancellationToken: cancellationToken);
      return response.MatchedCount !=0 && response.ModifiedCount !=0;
    }

    public async Task<List<Book>> GetAllAsync(int? skip = null, int? take = null, string? sortBy = null, string? sortOrder = null,
      string? titleFilter = null, int? yearFilter = null, CancellationToken cancellationToken = default)
    {
      var filterBuilder = Builders<Book>.Filter;
      var filter = filterBuilder.Empty;

      if (!string.IsNullOrEmpty(titleFilter))
        filter &= filterBuilder.Regex(b => b.Title, new MongoDB.Bson.BsonRegularExpression(titleFilter, "i"));

      if (yearFilter.HasValue)
      {
        var startDate = new DateTime(yearFilter.Value, 1, 1);
        var endDate = startDate.AddYears(1).AddTicks(-1);

        filter &= filterBuilder.And(
          filterBuilder.Gte(b => b.YearOfPublication, startDate),
          filterBuilder.Lte(b => b.YearOfPublication, endDate));
      }

      var query = booksCollection.Find(filter);

      if (!string.IsNullOrEmpty(sortBy))
      {
        var sortDefinition = sortOrder == "desc"
          ? Builders<Book>.Sort.Descending(sortBy)
          : Builders<Book>.Sort.Ascending(sortBy);

        query = query.Sort(sortDefinition);
      }

      if (skip.HasValue) query = query.Skip(skip.Value);
      if (take.HasValue) query = query.Limit(take.Value);

      return await query.ToListAsync(cancellationToken);
      
    }
  }
}
