using BookStore.Abstraction;
using BookStore.Domain;
using BookStore.Repositories;

namespace BookStore.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<string> AddBookAsync(Book book, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Title is required");

            return await _bookRepository.InsertAsync(book, cancellationToken);
        }

        public async Task<List<Book>> GetAllBooksAsync(CancellationToken cancellationToken)
        {
            return await _bookRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Book> GetBookByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetByIdAsync(id, cancellationToken);
        }
    }
}