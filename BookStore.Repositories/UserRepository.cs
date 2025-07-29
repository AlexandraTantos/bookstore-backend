using BookStore.Abstraction;
using BookStore.Domain;
using MongoDB.Driver;

namespace BookStore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabase database;
        private readonly IMongoCollection<User> usersCollection;

        public UserRepository(IDatabase database)
        {
            this.database = database;
            this.usersCollection = database.GetMongoCollection<User>("Users");
        }

        public async Task<string> InsertAsync(User user, CancellationToken cancellationToken)
        {
            user.Id = null; 
            await this.usersCollection.InsertOneAsync(user, cancellationToken: cancellationToken);
            return user.Id;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await this.usersCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Empty;
            return await this.usersCollection.Find(filter).ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
            var update = Builders<User>.Update
                .Set(u => u.Email, user.Email)
                .Set(u => u.PasswordHash, user.PasswordHash)
                .Set(u => u.Role, user.Role);

            var result = await this.usersCollection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            return result.MatchedCount > 0 && result.ModifiedCount > 0;
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            await this.usersCollection.DeleteOneAsync(filter, cancellationToken);
        }

        public async Task<User> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return await this.usersCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
