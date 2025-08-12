using BookStore.Domain;
using BookStore.Repositories;
using BookStore.Services;
using Moq;

namespace BookStore.Tests
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> mockRepo;
        private readonly BookService bookService;

        public BookServiceTests()
        {
            mockRepo = new Mock<IBookRepository>();
            bookService = new BookService(mockRepo.Object);
        }

        [Fact]
        public async Task AddBookAsync_ShouldReturnId_WhenValidBook()
        {
            var book = new Book
            {
                Id = "123",
                Title = "Test Book",
                YearOfPublication = new DateTime(2020, 1, 1),
                Genres = new List<string> { "Drama" }
            };

            mockRepo.Setup(r => r.InsertAsync(book, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(book.Id);

            var result = await bookService.AddBookAsync(book, CancellationToken.None);

            Assert.Equal("123", result);
        }

        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnBooks()
        {
            var books = new List<Book>
            {
                new Book { Id = "1", Title = "Book1", YearOfPublication = DateTime.Now, Genres = new List<string> { "Fiction" } },
                new Book { Id = "2", Title = "Book2", YearOfPublication = DateTime.Now, Genres = new List<string> { "Sci-Fi" } }
            };

            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(books);

            var result = await bookService.GetAllBooksAsync(CancellationToken.None);

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnBook_WhenBookExists()
        {
            var book = new Book { Id = "1", Title = "Book1", YearOfPublication = DateTime.Now, Genres = new List<string> { "Fiction" } };

            mockRepo.Setup(r => r.GetByIdAsync("1", It.IsAny<CancellationToken>()))
                    .ReturnsAsync(book);

            var result = await bookService.GetBookByIdAsync("1", CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("Book1", result.Title);
        }
       
        [Fact]
        public async Task DeleteBookByIdAsync_ShouldDelete_WhenBookExists()
        {
            var book = new Book 
            { 
                Id = "1", 
                Title = "Book1", 
                YearOfPublication = DateTime.Now, 
                Genres = new List<string> { "Fiction" } 
            };

            await bookService.AddBookAsync(book, CancellationToken.None);
            
            mockRepo.Setup(r => r.DeleteAsync("1", It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            
            mockRepo.Setup(r => r.GetByIdAsync("1", It.IsAny<CancellationToken>()))
                .ReturnsAsync((Book?)null);
            await bookService.DeleteBookByIdAsync("1", CancellationToken.None);

           var deletedBook = await bookService.GetBookByIdAsync("1", CancellationToken.None);
            Assert.Null(deletedBook);

           mockRepo.Verify(r => r.DeleteAsync("1", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBook_ShouldUpdate_WhenBookExists()
        {
            var book = new Book { Id = "1", Title = "Book1", YearOfPublication = DateTime.Now, Genres = new List<string> { "Fiction" } };
            await bookService.AddBookAsync(book, CancellationToken.None);
            mockRepo.Setup(r => r.GetByIdAsync("1", It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            book.Title = "Updated";
            await bookService.UpdateBookAsync(book, CancellationToken.None);
        
            var updatedBook = await bookService.GetBookByIdAsync("1", CancellationToken.None);
            Assert.Equal("Updated", updatedBook.Title);
        }
    }
}
