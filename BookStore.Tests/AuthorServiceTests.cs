using BookStore.Abstraction;
using BookStore.Domain;
using BookStore.Services;
using Moq;

namespace BookStore.Tests;

public class AuthorServiceTests
{
    private readonly Mock<IAuthorRepository> mockAuthorRepository;
    private readonly AuthorService authorService;

    public AuthorServiceTests()
    {
        mockAuthorRepository = new Mock<IAuthorRepository>();
        authorService = new AuthorService(mockAuthorRepository.Object);
    }

    [Fact]
    public async Task AddAuthorAsync_ShouldReturnId_WhenValidAuthor()
    {
        var author = new Author
        {
            Id = "123",
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateTime(2000, 1, 1),
            Nationality = "french",
            SpokenLanguages = new List<string> { "English" }
        };

        mockAuthorRepository.Setup(r => r.InsertAsync(author, It.IsAny<CancellationToken>())).ReturnsAsync(author.Id);
        var result = await authorService.AddAuthorAsync(author, CancellationToken.None);
        Assert.Equal("123", result);
    }
    
    [Fact]
    public async Task GetAllAuthorsAsync_ShouldReturnAuthors()
    {
        var authors = new List<Author>
        {
            new Author { Id = "1", FirstName = "Jim",LastName = "Doe", BirthDate = DateTime.Now,Nationality = "German", SpokenLanguages = new List<string> { "Fiction" } },
            new Author { Id = "2", FirstName = "Jenny",LastName = "Doe", BirthDate = DateTime.Now, Nationality = "French",SpokenLanguages = new List<string> { "Sci-Fi" } }
        };

        mockAuthorRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(authors);

        var result = await authorService.GetAllAuthorsAsync(CancellationToken.None);

        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async Task GetAuthorByIdAsync_ShouldReturnAuthor_WhenAuthorExists()
    {
        var author = new Author { Id = "1", FirstName = "John",LastName = "Doe", BirthDate = DateTime.Now, Nationality = "French", SpokenLanguages = new List<string> { "Fiction" } };

        mockAuthorRepository.Setup(r => r.GetByIdAsync("1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(author);

        var result = await authorService.GetAuthorAsync("1", CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
        Assert.Equal("French", result.Nationality);
    }

    [Fact]
    public async Task DeleteAuthorByIdAsync_ShouldDelete_WhenAuthorExists()
    {
        var author = new Author { Id = "1", FirstName = "John",LastName = "Doe", BirthDate = DateTime.Now, Nationality = "French", SpokenLanguages = new List<string> { "Fiction" } };
        
        await authorService.AddAuthorAsync(author, CancellationToken.None);
        
        mockAuthorRepository.Setup(r => r.DeleteAsync("1", It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        mockAuthorRepository.Setup(r => r.GetByIdAsync("1", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Author?)null);

        await authorService.DeleteAuthorByIdAsync("1", CancellationToken.None);
        var deletedAuthor =  await authorService.GetAuthorAsync("1", CancellationToken.None);
       
        Assert.Null(deletedAuthor);
        mockAuthorRepository.Verify(r => r.DeleteAsync("1", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAuthor_ShouldUpdate_WhenAuthorExists()
    {
        var author = new Author { Id = "1", FirstName = "John",LastName = "Doe", BirthDate = DateTime.Now, Nationality = "French", SpokenLanguages = new List<string> { "Fiction" } };
        
        await authorService.AddAuthorAsync(author, CancellationToken.None);
        mockAuthorRepository.Setup(r => r.GetByIdAsync("1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(author);

        author.FirstName = "Updated";
        await authorService.UpdateAuthorAsync(author, CancellationToken.None);
        var updatedAuthor = await authorService.GetAuthorAsync("1", CancellationToken.None);
        Assert.Equal("Updated", updatedAuthor.FirstName);
    }
}