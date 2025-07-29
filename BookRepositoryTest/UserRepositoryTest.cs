using BookStore.Abstraction;
using BookStore.Domain;
using BookStore.MongDb;
using BookStore.Repositories;
using MongoDB.Driver;

namespace BookRepositoryTest;

public class UserRepositoryTest :IDisposable
{
    private IDatabase database;
    private DatabaseConfiguration databaseConfiguration;
    private IUserRepository userRepository;

    public UserRepositoryTest()
    {
        databaseConfiguration = new DatabaseConfiguration
        {
            ConnectionString = "mongodb+srv://test:test@bookstorecluster.gb2qqmb.mongodb.net/",
            DatabaseName = "BookStore"
        };
        database = new Database(databaseConfiguration);
        userRepository = new UserRepository(this.database);
    }
    public void Dispose()
    {
        var collection = database.GetMongoCollection<User>("Users");
        collection.DeleteMany(Builders<User>.Filter.Empty);
    }
    [Theory]
    [InlineData("686e97f2a253c58714def25f", "alw@test", "473287f8298dba7163a897908958f7c0eae733e25d2e027992ea2edc9bed2fa8", "User")]
    [InlineData("686e97f2a253c58714def25f", "alexandra@test", "473287f8298dba7163a897908958f7c0eae733e25d2e027992ea2edc9bed2fa8", "Admin")]
    public async Task ShouldInsertUser(string id, string email, string password, string role)
    {
        var roleString = Enum.Parse<Role>(role);
        var user = new User
        {
            Email = email,
            PasswordHash = password,
            Role = roleString,
            Id = id,
        };

        var isInserted = await userRepository.InsertAsync(user, CancellationToken.None);
        Assert.False(string.IsNullOrWhiteSpace(isInserted));
    }
}