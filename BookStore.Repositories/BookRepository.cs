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

    public async Task<List<Book>> GetAllAsync(CancellationToken cancellationToken)
    {
      var filter = Builders<Book>.Filter.Empty;
      List<Book> booksFromDb = await this.booksCollection.Find(filter)
      .ToListAsync(cancellationToken);
      return booksFromDb;
    }

    public async Task<Book> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
      var filter = Builders<Book>.Filter.Eq(x => x.Id, id);
      Book book = await this.booksCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
      return book;
    }

    public async Task<string> InsertAsync(Book item, CancellationToken cancellationToken)
    {
      item.Id = null;
      await this.booksCollection.InsertOneAsync(item, cancellationToken);
      return item.Id;
    }

    public async Task<bool> UpdateAsync(Book item, CancellationToken cancellationToken)
    {
      //patch -> 
      //put -> 
      var filter = Builders<Book>.Filter.Eq(x => x.Id, item.Id);
      var update = Builders<Book>.Update.Set(x => x.AuthorId, item.AuthorId)
                                        .Set(x => x.Genres, item.Genres)
                                        .Set(x => x.YearOfPublication, item.YearOfPublication)
                                        .Set(x => x.Title, item.Title);

      //if we want to specify which properties to update
      var response = await this.booksCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
      
      //if we want to replace the whole document
      //await this.booksCollection.ReplaceOneAsync(filter, item, cancellationToken: cancellationToken);
      return response.MatchedCount !=0 && response.ModifiedCount !=0;
    }
  }
}
