using BookStore.Abstraction;
using BookStore.Domain;
using MongoDB.Driver;

namespace BookStore.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private IDatabase database;
    private IMongoCollection<Author> authorCollection;

    public AuthorRepository(IDatabase database)
    {
        this.database = database;
        this.authorCollection = database.GetMongoCollection<Author>("Authors");
    }
    public async Task<string> InsertAsync(Author item, CancellationToken cancellationToken)
    {
        item.Id = null;
        await this.authorCollection.InsertOneAsync(item, cancellationToken);
        return item.Id;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<Author>.Filter.Eq(x => x.Id, id);
        await this.authorCollection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<Author> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<Author>.Filter.Eq(x => x.Id, id);
        Author author = await this.authorCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        return author;
    }

    public async Task<List<Author>> GetAllAsync(CancellationToken cancellationToken)
    {
       var filter = Builders<Author>.Filter.Empty;
       List<Author> authors = await this.authorCollection.Find(filter).Limit(10).ToListAsync(cancellationToken);
       return authors;
    }

    public async Task<bool> UpdateAsync(Author item, CancellationToken cancellationToken)
    {
        var filter = Builders<Author>.Filter.Eq(x => x.Id, item.Id);
        var update = Builders<Author>.Update.Set(x => x.FirstName, item.FirstName)
            .Set(x => x.LastName, item.LastName)
            .Set(x => x.BirthDate, item.BirthDate)
            .Set(x => x.SpokenLanguages, item.SpokenLanguages)
            .Set(x => x.Nationality, item.Nationality);
        var response = await this.authorCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return response.MatchedCount != 0 && response.ModifiedCount != 0; 
    }
}