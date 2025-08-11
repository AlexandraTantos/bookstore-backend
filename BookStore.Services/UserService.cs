using BookStore.Abstraction;
using BookStore.Domain;

namespace BookStore.Services;

public class UserService(IUserRepository userRepository)
{
    public async Task<string> RegisterUserAsync(User user, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(user.Email))
            throw new ArgumentException("Email is required");

        return await userRepository.InsertAsync(user, cancellationToken);
    }
}