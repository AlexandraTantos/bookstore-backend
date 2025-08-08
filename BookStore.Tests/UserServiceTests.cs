using BookStore.Abstraction;
using BookStore.Domain;
using BookStore.Services;
using Moq;
using Xunit;

namespace BookStore.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> mockRepo;
    private readonly UserService userService;

    public UserServiceTests()
    {
        mockRepo = new Mock<IUserRepository>();
        userService = new UserService(mockRepo.Object);
    }

    [Theory]
    [InlineData("686e97f2a253c58714def25f", "alw@test", "somepasswordhash", "User")]
    [InlineData("686e97f2a253c58714def25f", "alexandra@test", "somepasswordhash", "Admin")]
    public async Task RegisterUserAsync_ShouldInsertUser(string id, string email, string password, string role)
    {
        var user = new User
        {
            Id = id,
            Email = email,
            PasswordHash = password,
            Role = Enum.Parse<Role>(role)
        };

        mockRepo.Setup(r => r.InsertAsync(user, It.IsAny<CancellationToken>()))
            .ReturnsAsync(id);

        var result = await userService.RegisterUserAsync(user, CancellationToken.None);

        Assert.Equal(id, result);
        mockRepo.Verify(r => r.InsertAsync(It.Is<User>(u => u.Email == email), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldThrowException_WhenEmailIsEmpty()
    {
        var user = new User
        {
            Id = "123",
            Email = "",
            PasswordHash = "hash",
            Role = Role.User
        };

        await Assert.ThrowsAsync<ArgumentException>(() => userService.RegisterUserAsync(user, CancellationToken.None));
    }
}