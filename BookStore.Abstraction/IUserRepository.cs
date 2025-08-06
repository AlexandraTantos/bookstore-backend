using BookStore.Domain;

namespace BookStore.Abstraction;

public interface IUserRepository : IRepository<User>
{
    Task<User> FindByEmailAsync(string email);
}