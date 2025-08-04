using BookStore.Abstraction;
using BookStore.Domain;
using MongoDB.Bson;
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
        return await GetAllAsync(null, null, null, null, null, null, null,cancellationToken);
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

    public async Task<List<Author>> GetAllAsync(int? skip = null, int? take = null, string? sortBy = null, string? sortOrder = null,
        string? firstNameFilter = null, string? lastNameFilter = null, int? birthDateFilter = null,
        CancellationToken cancellationToken = default)
    {
        var filterBuilder = Builders<Author>.Filter;
        var filter = filterBuilder.Empty;
        
        if (!string.IsNullOrEmpty(firstNameFilter))
            filter &= filterBuilder.Regex(b => b.FirstName, new BsonRegularExpression(firstNameFilter, "i"));


        if (!string.IsNullOrEmpty(lastNameFilter))
            filter &= filterBuilder.Regex(b => b.LastName, new BsonRegularExpression(lastNameFilter, "i"));
        
        if (birthDateFilter.HasValue)
        {
            var startDate = new DateTime(birthDateFilter.Value, 1, 1);
            var endDate = startDate.AddYears(1).AddTicks(-1);

            filter &= filterBuilder.And(
                filterBuilder.Gte(b => b.BirthDate, startDate),
                filterBuilder.Lte(b => b.BirthDate, endDate));
        }
        var query = authorCollection.Find(filter);

        if (!string.IsNullOrEmpty(sortBy))
        {
            var sortDefinition = sortOrder == "desc"
                ? Builders<Author>.Sort.Descending(sortBy)
                : Builders<Author>.Sort.Ascending(sortBy);

            query = query.Sort(sortDefinition);
        }

        if (skip.HasValue) query = query.Skip(skip.Value);
        if (take.HasValue) query = query.Limit(take.Value);

        return await query.ToListAsync(cancellationToken);
    }
}