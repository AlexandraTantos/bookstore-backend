using BookStore.Abstraction;
using BookStore.Domain;

namespace BookStore.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> FindByEmailAsync(string email);
}