using BookStore.Abstraction;
using BookStore.Domain;

namespace BookStore.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> RegisterUserAsync(User user, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(user.Email))
            throw new ArgumentException("Email is required");

        return await _userRepository.InsertAsync(user, cancellationToken);
    }
}