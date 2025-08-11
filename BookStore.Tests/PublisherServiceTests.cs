using BookStore.Abstraction;
using BookStore.Domain;
using BookStore.Services;
using Moq;

namespace BookStore.Tests;

public class PublisherServiceTests
{
    private readonly Mock<IPublisherRepository> mockPublisherRepo;
    private readonly PublisherService publisherService;

    public PublisherServiceTests()
    {
        mockPublisherRepo = new Mock<IPublisherRepository>();
        publisherService = new PublisherService(mockPublisherRepo.Object);
    }
    [Fact]
    public async Task AddPublisherAsync_ShouldReturnId_WhenValidPublisher()
    {
        var publisher = new Publisher
        {
            Id = "123",
            Name = "John Doe",
            Address = "123 St Main",
            Email = "test@yahoo.com"
        };

        mockPublisherRepo.Setup(r => r.InsertAsync(publisher, It.IsAny<CancellationToken>()))
            .ReturnsAsync(publisher.Id);

        var result = await publisherService.AddPublisherAsync(publisher, CancellationToken.None);

        Assert.Equal("123", result);
    }

    [Fact]
    public async Task GetAllPublishersAsync_ShouldReturnPublishers()
    {
        var publishers = new List<Publisher>
        {
            new Publisher { Id = "1", Name = "John Doe", Address = "St Main Street", Email = "test@yahoo.com"},
            new Publisher { Id = "2", Name = "Jim Doe", Address = "123 St Main Streer",Email = "Test@gmail.com"}
        };

        mockPublisherRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(publishers);

        var result = await publisherService.GetAllPublishersAsync(CancellationToken.None);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetPublisherByIdAsync_ShouldReturnPublisher_WhenPublisherExists()
    {
        var publisher = new Publisher { Id = "1", Name = "John Doe", Address = "St Main Street", Email = "test@yahoo.com"};

        mockPublisherRepo.Setup(r => r.GetByIdAsync("1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(publisher);

        var result = await publisherService.GetPublisherByIdAsync("1", CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
        Assert.Equal("St Main Street", result.Address);
        Assert.NotNull(result.Email);
    }
}
