using BookStore.Abstraction;
using BookStore.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookStore.Repositories;

public class PublisherRepository :IPublisherRepository
{
    private IDatabase database;
    private IMongoCollection<Publisher> publishersCollection;

    public PublisherRepository(IDatabase database)
    {
        this.database = database;
        this.publishersCollection = database.GetMongoCollection<Publisher>("Publishers");
    }
    public async Task<string> InsertAsync(Publisher item, CancellationToken cancellationToken)
    {
        item.Id = null;
        await this.publishersCollection.InsertOneAsync(item,cancellationToken);
        return item.Id;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<Publisher>.Filter.Eq(x => x.Id, id);
        await this.publishersCollection.DeleteOneAsync(filter, cancellationToken);
    }

    public async  Task<Publisher> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<Publisher>.Filter.Eq(x => x.Id, id);
        Publisher publisher = await this.publishersCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        return publisher;
    }

    public async Task<List<Publisher>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await GetAllAsync(null, null, null, null, null,cancellationToken);
    }

    public async Task<bool> UpdateAsync(Publisher item, CancellationToken cancellationToken)
    {
        var filter = Builders<Publisher>.Filter.Eq(x => x.Id, item.Id);
        var update = Builders<Publisher>.Update.Set(x => x.Name, item.Name)
            .Set(x => x.Email, item.Email)
            .Set(x => x.Address, item.Address);
        var result = await this.publishersCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.MatchedCount != 0 && result.ModifiedCount != 0;
    }

    public async Task<List<Publisher>> GetAllAsync(int? skip = null, int? take = null, string? sortBy = null, string? sortOrder = null,
        string? nameFilter = null, CancellationToken cancellationToken = default)
    {
        var filterBuilder = Builders<Publisher>.Filter;
        var filter = filterBuilder.Empty;
        
        if (!string.IsNullOrEmpty(nameFilter))
        {
            filter &= filterBuilder.Regex(b => b.Name, new BsonRegularExpression(nameFilter, "i"));
        }
        var query = publishersCollection.Find(filter);

        if (!string.IsNullOrEmpty(sortBy))
        {
            var sortDefinition = sortOrder == "desc"
                ? Builders<Publisher>.Sort.Descending(sortBy)
                : Builders<Publisher>.Sort.Ascending(sortBy);

            query = query.Sort(sortDefinition);
        }

        if (skip.HasValue) query = query.Skip(skip.Value);
        if (take.HasValue) query = query.Limit(take.Value);

        return await query.ToListAsync(cancellationToken);
    }
}