using BookStore.Abstraction;
using BookStore.Domain;
using BookStore.MongDb;
using BookStore.Repositories;
using MongoDB.Driver;

namespace BookRepositoryTest
{
  public class BookRepositoryTest :IDisposable
  {
    private IDatabase database;
    private DatabaseConfiguration databaseConfiguration;
    private IBookRepository bookRepository;

    public BookRepositoryTest()
    {
      databaseConfiguration = new DatabaseConfiguration
      {
        ConnectionString = "mongodb://localhost:27017",
        DatabaseName = "BookStore"
      };

      database = new Database(databaseConfiguration);
      bookRepository = new BookRepository(this.database);
    }

    public void Dispose()
    {
      var collection = database.GetMongoCollection<Book>("Books");
      collection.DeleteMany(_ => true);
    }

    [Theory]
    [InlineData("686cc19f67dbe16840024cad","Lord of the rings", "2020-05-04 02:23:43", "sci fi","comedy") ]
    [InlineData("686cc19f6734745445324cad", "Test2", "2021-05-04 02:23:43", "stest", "comedy")]
    [InlineData("686cc19f67dbe16840024cad", "LTets3", "2020-05-04 02:23:43", "horror", "romance")]
    public async Task ShouldInsertBook(string id, string title, DateTime yearOfPublication,params string[] genres)
    {
      var book = new Book
      {
        Title = title,
        YearOfPublication = yearOfPublication,
        Genres = genres.ToList(),
        Id = id,
      };

      var isInserted = await bookRepository.InsertAsync(book, CancellationToken.None);
      Assert.False(string.IsNullOrWhiteSpace(isInserted));
    }
  }
}